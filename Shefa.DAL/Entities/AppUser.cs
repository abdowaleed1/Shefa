using Microsoft.AspNetCore.Identity;
using Models.Enums;
using Models.InterFaces;

namespace Models.Entities
{
    public class AppUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>();
    }

}
