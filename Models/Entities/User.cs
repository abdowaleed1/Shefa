using Models.Enums;
using Models.InterFaces;

namespace Models.Entities
{
    public class User : BaseEntity, ISoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; } = false; 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public Doctor DoctorProfile { get; set; }
        public Patient PatientProfile { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public ICollection<Clinic> Clinics { get; set; } = new HashSet<Clinic>();
    }

}
