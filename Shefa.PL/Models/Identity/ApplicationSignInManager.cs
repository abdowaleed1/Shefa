using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SmartHealthcare.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartHealthcare.Data
{
    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            // Calls the GenerateUserIdentityAsync method in ApplicationUser
            // Note: The UserManager must be explicitly cast to the concrete ApplicationUserManager type
            // to correctly resolve the overloaded method call, which fixes CS0121.
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        // Static method required by OWIN (app.CreatePerOwinContext)
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(
                context.GetUserManager<ApplicationUserManager>(),
                context.Authentication);
        }
    }
}
