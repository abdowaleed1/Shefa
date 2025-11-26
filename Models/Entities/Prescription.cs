namespace Models.Entities
{
    public class Prescription : BaseEntity 
    {
        public string PrescriptionImageURL { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }


}
