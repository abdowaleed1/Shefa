namespace Models.Entities
{
    public class Review : BaseEntity
    {
        public string? DoctorComment { get; set; }
        public string? ClinicComment { get; set; }
        public int DoctorRating { get; set; } 
        public int ClinicRating { get; set; } 
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string ClinicId { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
        public Patient Patient { get; set; }
    }

}
