namespace Models.Entities
{
    public class Doctor : BaseEntity
    {
        public string Specialty { get; set; }
        public decimal ConsultationPrice { get; set; }
        public int ConsultationTime { get; set; } 
        public double AverageReviewRate { get; set; }
        public int CountOfReviews { get; set; }
        public bool IsVerified { get; set; }
        public string? Biography { get; set; }
        public string? Education { get; set; } 
        public int? ExperienceYears { get; set; } 
        public string ClinicId { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Clinic Clinic { get; set; }
        public ICollection<Slot> Slots { get; set; } = new HashSet<Slot>();
       
        public ICollection<DoctorSchedule> DoctorSchedules { get; set; } = new HashSet<DoctorSchedule>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
        public ICollection<DiagnosisReport> DiagnosisReports { get; set; } = new HashSet<DiagnosisReport>();
    }
    
}
