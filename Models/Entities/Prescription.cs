namespace Models.Entities
{
    public class Prescription : BaseEntity 
    {
        public string PrescriptionImageURL { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }


}
