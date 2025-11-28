namespace Models.Entities
{
    public class DiagnosisReport : BaseEntity 
    {
        public string ReportType { get; set; }
        public string ReportURL { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

    }

}
