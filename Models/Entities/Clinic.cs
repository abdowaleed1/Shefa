namespace Models.Entities
{
    public class Clinic : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


        public int ManagerId { get; set; }
        public User Manager { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }


}
