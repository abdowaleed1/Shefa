namespace Models.Entities
{
    public class DiagnosisReport : BaseEntity 
    {
        public string ReportType { get; set; }
        public string ReportURL { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

    }

}
