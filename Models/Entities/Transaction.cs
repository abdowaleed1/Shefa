namespace Models.Entities
{
    public class Transaction : BaseEntity 
    {
        public decimal Amount { get; set; }
        public string Type { get; set; } 
        public string Status { get; set; } 
        public string TransactionReference { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

    }

}
