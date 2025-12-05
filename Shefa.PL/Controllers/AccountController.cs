using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SmartHealthcare.Data;
using SmartHealthcare.Models;

namespace SmartHealthcare.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ApplicationDbContext _db;

        public AccountController()
        {
            _db = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _db = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        // ===============================
        // Login (GET)
        // ===============================
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // Prevent caching of login page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();

            // Clear old anti-forgery cookie to prevent mismatches
            if (Request.Cookies["__RequestVerificationToken"] != null)
            {
                var cookie = new HttpCookie("__RequestVerificationToken") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(cookie);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // ===============================
        // Login (POST)
        // ===============================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Admin Login
            if (model.Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                if (model.Password == "Admin@2025")
                {
                    var adminUser = await UserManager.FindByEmailAsync(model.Email);

                    if (adminUser == null)
                    {
                        adminUser = new ApplicationUser
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            FullName = "System Administrator",
                            RegistrationDate = DateTime.Now
                        };

                        var createResult = await UserManager.CreateAsync(adminUser, model.Password);
                        if (createResult.Succeeded)
                            await UserManager.AddToRoleAsync(adminUser.Id, "Admin");
                        else
                        {
                            AddErrors(createResult);
                            return View(model);
                        }
                    }

                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    await SignInManager.SignInAsync(adminUser, isPersistent: model.RememberMe, rememberBrowser: false);

                    return RedirectToAction("Dashboard", "Admin");
                }

                ModelState.AddModelError("", "Invalid admin login attempt.");
                return View(model);
            }

            // Regular User Login
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user != null && await UserManager.CheckPasswordAsync(user, model.Password))
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                await SignInManager.SignInAsync(user, isPersistent: model.RememberMe, rememberBrowser: false);

                if (await UserManager.IsInRoleAsync(user.Id, "Doctor"))
                    return RedirectToAction("Dashboard", "Doctor");

                if (await UserManager.IsInRoleAsync(user.Id, "Patient"))
                    return RedirectToAction("Dashboard", "Patient");

                return RedirectToAction("Login", "Account");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // ===============================
        // Register (GET)
        // ===============================
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // ===============================
        // Register (POST)
        // ===============================
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Email.Equals("admin@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Cannot register as admin. Please contact system administrator.");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                RegistrationDate = DateTime.Now
            };

            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string role = model.Role == "Doctor" ? "Doctor" : "Patient";
                await UserManager.AddToRoleAsync(user.Id, role);

                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                if (role == "Doctor")
                    return RedirectToAction("CompleteProfile", "Doctor"); // ✅ FIXED (was "Doctors")

                return RedirectToAction("CompleteProfile", "Patient");
            }

            AddErrors(result);
            return View(model);
        }

        // ===============================
        // LogOff (POST)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();

            if (Request.Cookies["__RequestVerificationToken"] != null)
            {
                var cookie = new HttpCookie("__RequestVerificationToken") { Expires = DateTime.Now.AddDays(-1) };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login", "Account");
        }

        // ===============================
        // Helpers
        // ===============================
        private IAuthenticationManager AuthenticationManager =>
            HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userManager?.Dispose();
                _signInManager?.Dispose();
                _db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
