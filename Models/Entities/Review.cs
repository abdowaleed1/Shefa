namespace Models.Entities
{
    public class Review : BaseEntity
    {
        public string? DoctorComment { get; set; }
        public string? ClinicComment { get; set; }
        public int DoctorRating { get; set; } 
        public int ClinicRating { get; set; } 
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ClinicId { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
        public Patient Patient { get; set; }
    }

}
