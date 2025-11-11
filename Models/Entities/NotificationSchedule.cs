using Models.Enums;

namespace Models.Entities
{
    public class NotificationSchedule : BaseEntity 
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public NotificationRecurrence RecurrenceType { get; set; } 
        public int Frequency { get; set; } 
        public NotificationEventType NotificationType { get; set; } 
        public DateTime NextRunDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelivered { get; set; }
    }

}
