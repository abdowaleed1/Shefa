using Models.Enums;

namespace Models.Entities
{
    public class Transaction : BaseEntity 
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; } 
        public PaymentStatus Status { get; set; } 
        public string? TransactionReference { get; set; }
        public string PatientId { get; set; }
        public string AppointmentId { get; set; }

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }

    }

}
