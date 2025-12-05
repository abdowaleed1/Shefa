using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace SmartHealthcare.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        // ✅ Make Nullable to avoid SQL DateTime2 error
        public DateTime? RegistrationDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            if (!string.IsNullOrEmpty(FullName))
            {
                userIdentity.AddClaim(new Claim("FullName", this.FullName));
            }
            return userIdentity;
        }
    }
}
