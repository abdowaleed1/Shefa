using Shefa.BLL.Interfaces;
using Models.Contexts;
using Models.Entities;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ShefaContext _context;
        public UnitOfWork(ShefaContext context)
        {
            _context = context;
            Appointments = new Repository<Appointment>(_context);
            AppRoles = new Repository<AppRole>(_context);
            Clinics = new Repository<Clinic>(_context);
            DiagnosisReports = new Repository<DiagnosisReport>(_context);
            Doctors = new Repository<Doctor>(_context);
            DoctorSchedules = new Repository<DoctorSchedule>(_context);
            NotificationSchedules = new Repository<NotificationSchedule>(_context);
            Patients = new Repository<Patient>(_context);
            PatientNotes = new Repository<PatientNotes>(_context);
            Prescriptions = new Repository<Prescription>(_context);
            Reviews = new Repository<Review>(_context);
            Transactions = new Repository<Transaction>(_context);
            AppUsers = new Repository<AppUser>(_context);
            Slots = new Repository<Slot>(_context);

        }
        public IRepository<Appointment> Appointments { get; }

        public IRepository<AppRole> AppRoles { get; }

        public IRepository<Clinic> Clinics { get; }

        public IRepository<DiagnosisReport> DiagnosisReports { get; }

        public IRepository<Doctor> Doctors { get; }

        public IRepository<DoctorSchedule> DoctorSchedules { get; }

        public IRepository<NotificationSchedule> NotificationSchedules { get; }

        public IRepository<Patient> Patients { get; }

        public IRepository<PatientNotes> PatientNotes { get; }

        public IRepository<Prescription> Prescriptions { get; }

        public IRepository<Review> Reviews { get; }

        public IRepository<Transaction> Transactions { get; }

        public IRepository<AppUser> AppUsers { get; }
        public IRepository<Slot> Slots { get; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
