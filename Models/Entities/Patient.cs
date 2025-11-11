namespace Models.Entities
{
    public class Patient : BaseEntity
    {

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }

        // Foreign Key to User
        public int UserId { get; set; }
        public User User { get; set; }

        // Navigation properties
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        public ICollection<DiagnosisReport> DiagnosisReports { get; set; } = new HashSet<DiagnosisReport>();
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public ICollection<PatientNotes> PatientNotes { get; set; } = new HashSet<PatientNotes>();
        public ICollection<NotificationSchedule> NotificationSchedules { get; set; } = new HashSet<NotificationSchedule>();
    }

}
