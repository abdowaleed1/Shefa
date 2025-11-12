namespace Models.Entities
{
    public class Prescription : BaseEntity 
    {
        public string PrescriptionImageURL { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }


}
