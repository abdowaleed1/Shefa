using Models.Enums;
using Models.InterFaces;

namespace Models.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>();
    }

}
