using Models.Enums;

namespace Models.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string ConfirmationCode { get; set; }
        public AppointmentStatus Status { get; set; }
        public ConsultationType ConsultationType { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Transaction Transaction { get; set; }

        public ICollection<PatientNotes> PatientNotes { get; set; } = new HashSet<PatientNotes>();
    }

}
