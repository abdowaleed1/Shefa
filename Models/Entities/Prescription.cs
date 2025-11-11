namespace Models.Entities
{
    public class Prescription : BaseEntity 
    {
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public int DurationDays { get; set; }
        public bool IsActive { get; set; }
        public int DiagnosisReportId { get; set; }
        public DiagnosisReport DiagnosisReport { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }

}
