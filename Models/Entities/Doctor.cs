namespace Models.Entities
{
    public class Doctor : BaseEntity
    {
        public string Specialty { get; set; }
        public string Qualification { get; set; }
        public decimal ConsultationPrice { get; set; }
        public int ConsultationTime { get; set; } 
        public double AverageReviewRate { get; set; }
        public bool IsVerified { get; set; }
        public string Biography { get; set; }
        public string SubSpecialty { get; set; } 
        public string Education { get; set; } 
        public int ExperienceYears { get; set; } 

        public int UserId { get; set; }
        public User User { get; set; }
        public int? ClinicId { get; set; }
        public Clinic Clinic { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        public ICollection<DiagnosisReport> DiagnosisReports { get; set; } = new HashSet<DiagnosisReport>();
    }

}
