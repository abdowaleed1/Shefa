using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Reflection;
namespace Models.Contexts
{
    public class ShefaContext: IdentityDbContext<AppUser, AppRole, string>
    {
        public ShefaContext(DbContextOptions<ShefaContext> options) : base(options) { }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }

        public DbSet<DiagnosisReport> DiagnosisReports { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PatientNotes> PatientNotes { get; set; }
        public DbSet<NotificationSchedule> NotificationSchedules { get; set; }
        public DbSet<Slot> Slots { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
