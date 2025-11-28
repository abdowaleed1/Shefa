using Models.Enums;
using Models.InterFaces;

namespace Models.Entities
{
    public class DoctorSchedule : BaseEntity
    {
        public string DoctorId { get; set; }
        public string ClinicId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DayOfWeek AvailableDays { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
        public int SlotDurationMinutes { get; set; }
    }

}
