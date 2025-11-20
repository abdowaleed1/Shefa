namespace Models.Entities
{
    public class Review : BaseEntity
    {
        public string? DoctorComment { get; set; }
        public string? ClinicComment { get; set; }
        public int DoctorRating { get; set; } 
        public int ClinicRating { get; set; } 
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ClinicId { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
        public Patient Patient { get; set; }
    }

}
