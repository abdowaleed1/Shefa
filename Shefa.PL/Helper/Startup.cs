using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SmartHealthcare.Data;
using SmartHealthcare.Models;
using System;

[assembly: OwinStartup(typeof(SmartHealthcare.Startup))]

namespace SmartHealthcare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Configure authentication + create default roles and admin
            ConfigureAuth(app);
            CreateRolesAndAdminUser();
        }

        // 🔐 Configure Application Authentication
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromDays(7),
                SlidingExpiration = true,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            // For external login providers (optional)
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        // 👑 Create Default Roles and Admin Account
        private void CreateRolesAndAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // ✅ Ensure base roles exist
                string[] roleNames = { "Admin", "Doctor", "Patient" };

                foreach (var roleName in roleNames)
                {
                    if (!roleManager.RoleExists(roleName))
                    {
                        var role = new IdentityRole(roleName);
                        var roleResult = roleManager.Create(role);

                        if (!roleResult.Succeeded)
                        {
                            throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", roleResult.Errors)}");
                        }
                    }
                }

                // ✅ Create default admin account (if not already present)
                string adminEmail = "admin@gmail.com";
                string adminPassword = "Admin@2025";

                var adminUser = userManager.FindByEmail(adminEmail);

                if (adminUser == null)
                {
                    var newAdmin = new ApplicationUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        FullName = "System Administrator",
                        RegistrationDate = DateTime.Now
                    };

                    var createUserResult = userManager.Create(newAdmin, adminPassword);
                    if (createUserResult.Succeeded)
                    {
                        userManager.AddToRole(newAdmin.Id, "Admin");
                    }
                    else
                    {
                        throw new Exception("⚠️ Admin creation failed: " + string.Join(", ", createUserResult.Errors));
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
