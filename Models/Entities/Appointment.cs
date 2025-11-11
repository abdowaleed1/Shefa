using Models.Enums;

namespace Models.Entities
{
    public class Appointment : BaseEntity
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; }
        public ConsultationType ConsultationType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public string ConfirmationCode { get; set; }
        public decimal Total { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }


        public DiagnosisReport DiagnosisReport { get; set; }
        public ICollection<PatientNotes> PatientNotes { get; set; } = new HashSet<PatientNotes>();
        public ICollection<NotificationSchedule> NotificationSchedules { get; set; } = new HashSet<NotificationSchedule>();
    }

}
