
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartHealthcare.Models;

namespace SmartHealthcare.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Data Source=.;Initial Catalog=ShefaTemp3;Integrated Security=True;Encrypt=True;TrustServerCertificate=True", throwIfV1Schema: false)
        {
            // Disable EF model compatibility & migration checks entirely
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // --- DbSets for all entities ---
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<MedicalReport> MedicalReports { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========================================
            // APPOINTMENT RELATIONSHIPS - FIXED
            // ========================================

            // Appointment -> Doctor
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Doctor)
                .WithMany(d => d.Appointments)  // ✅ FIXED: Specify the collection
                .HasForeignKey(a => a.DoctorID)
                .WillCascadeOnDelete(false);

            // Appointment -> Patient
            modelBuilder.Entity<Appointment>()
                .HasRequired(a => a.Patient)
                .WithMany(p => p.Appointments)  // ✅ FIXED: Specify the collection
                .HasForeignKey(a => a.PatientID)
                .WillCascadeOnDelete(false);

            // ========================================
            // PRESCRIPTION RELATIONSHIPS - FIXED
            // ========================================

            // Prescription -> Doctor
            modelBuilder.Entity<Prescription>()
                .HasRequired(p => p.Doctor)
                .WithMany()  // Doctor doesn't have Prescriptions collection in model
                .HasForeignKey(p => p.DoctorID)
                .WillCascadeOnDelete(false);

            // Prescription -> Patient
            modelBuilder.Entity<Prescription>()
                .HasRequired(p => p.Patient)
                .WithMany(pat => pat.Prescriptions)  // ✅ Patient has Prescriptions
                .HasForeignKey(p => p.PatientID)
                .WillCascadeOnDelete(false);

            // Prescription -> Appointment
            modelBuilder.Entity<Prescription>()
                .HasRequired(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentID)
                .WillCascadeOnDelete(false);

            // ========================================
            // BILL RELATIONSHIPS
            // ========================================

            modelBuilder.Entity<Bill>()
                .HasRequired(b => b.Appointment)
                .WithMany(a => a.Bills)
                .HasForeignKey(b => b.AppointmentID)
                .WillCascadeOnDelete(false);

            // ========================================
            // MEDICAL REPORT RELATIONSHIPS
            // ========================================

            modelBuilder.Entity<MedicalReport>()
                .HasRequired(mr => mr.Appointment)
                .WithMany(a => a.Reports)
                .HasForeignKey(mr => mr.AppointmentID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MedicalReport>()
                .HasRequired(mr => mr.Doctor)
                .WithMany()  // Doctor doesn't have MedicalReports collection
                .HasForeignKey(mr => mr.DoctorID)
                .WillCascadeOnDelete(false);

            // ========================================
            // SCHEDULE RELATIONSHIPS
            // ========================================

            modelBuilder.Entity<Schedule>()
                .HasRequired(s => s.Doctor)
                .WithMany(d => d.Schedules)
                .HasForeignKey(s => s.DoctorID)
                .WillCascadeOnDelete(false);

            // ========================================
            // IDENTITY RELATIONSHIPS
            // ========================================

            // Doctor -> ApplicationUser (Optional)
            modelBuilder.Entity<Doctor>()
                .HasOptional(d => d.ApplicationUser)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);

            // Patient -> ApplicationUser (Optional)
            modelBuilder.Entity<Patient>()
                .HasOptional(p => p.ApplicationUser)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}