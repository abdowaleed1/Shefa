using Models.Enums;

namespace Models.Entities
{
    public class NotificationSchedule : BaseEntity 
    {
        public NotificationRecurrence RecurrenceType { get; set; } 
        public int Frequency { get; set; } 
        public string MedicationName { get; set; } 
        public DateTime NextRunDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsDelivered { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

    }

}
