using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models.Contexts
{
    public class ShefaContext:DbContext
    {
        public DbSet<User> Users { get; set; }
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ShefaDB;Trusted_Connection=True;");
        }
    }
}
