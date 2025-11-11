namespace Models.Entities
{
    public class DiagnosisReport : BaseEntity 
    {
        public string ReportTitle { get; set; }
        public string ReportContent { get; set; }
     
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
    }

}
