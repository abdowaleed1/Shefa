using Models.Entities;
namespace Shefa.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository <Appointment> Appointments { get; }
        public IRepository <Slot> Slots { get; }

        public IRepository <AppRole> AppRoles { get; }

        public IRepository <Clinic> Clinics { get; }

        public IRepository <DiagnosisReport> DiagnosisReports { get; }

        public IRepository <Doctor> Doctors { get; }

        public IRepository <DoctorSchedule> DoctorSchedules { get; }

        public IRepository <NotificationSchedule> NotificationSchedules { get; }

        public IRepository <Patient> Patients { get; }

        public IRepository <PatientNotes> PatientNotes { get; }
        
        public IRepository <Prescription> Prescriptions { get; }
        
        public IRepository <Review> Reviews { get; }
        
        public IRepository <Transaction> Transactions { get; }
        
        public IRepository <AppUser> AppUsers { get; }
        
        int Save();
    }
}
