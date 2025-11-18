using Models.Enums;

namespace Models.Entities
{
    public class Transaction : BaseEntity 
    {
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; } 
        public PaymentStatus Status { get; set; } 
        public string? TransactionReference { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }

    }

}
