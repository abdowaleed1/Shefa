using Models.Enums;

namespace Models.Entities
{
    public class Patient : BaseEntity
    {

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int UserId { get; set; }
        
        public User User { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        public ICollection<DiagnosisReport> DiagnosisReports { get; set; } = new HashSet<DiagnosisReport>();
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public ICollection<PatientNotes> PatientNotes { get; set; } = new HashSet<PatientNotes>();
        public ICollection<NotificationSchedule> NotificationSchedules { get; set; } = new HashSet<NotificationSchedule>();
    }

}
