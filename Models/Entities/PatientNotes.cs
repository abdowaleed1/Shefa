using Models.Enums;

namespace Models.Entities
{
    public class PatientNotes : BaseEntity 
    {
        public Guid PatientId { get; set; }
        public Guid AppointmentId { get; set; }
        public string NoteContent { get; set; }
        public PatientNoteType NoteType { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }
}
