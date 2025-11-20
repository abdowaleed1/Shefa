using Models.InterFaces;

namespace Models.Entities
{
    public class DoctorSchedule : BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Guid ClinicId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
    }

}
