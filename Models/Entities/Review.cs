namespace Models.Entities
{
    public class Review : BaseEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int Rating { get; set; } 
        public string Comment { get; set; }
    }

}
