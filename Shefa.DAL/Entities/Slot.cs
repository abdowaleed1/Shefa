namespace Models.Entities
{
    public class Slot : BaseEntity
    {
        public string DoctorId { get; set; }
        public string ClinicId { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsBooked { get; set; } = false;
        public bool IsBlocked { get; set; } = false; 
        public Appointment Appointment { get; set; }
    }

}
