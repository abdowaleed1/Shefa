using Models.InterFaces;

namespace Models.Entities
{
    public class DoctorSchedule : BaseEntity
    {
        public string DoctorId { get; set; }
        public string ClinicId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
    }

}
