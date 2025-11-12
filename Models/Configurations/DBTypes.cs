using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Configurations
{
    public static class DBTypes
    {
        public const string Int = "int";
        public const string Bit = "bit";
        public const string Date = "date";
        public const string Time = "time";
        public const string DateTime2 = "datetime2";
        public const string Float = "float";
        public const string Money = "money";
        public const string Decimal18_2 = "decimal(18,2)";
        public const string nvarchar20 = "nvarchar(20)";
        public const string nvarchar50 = "nvarchar(50)";
        public const string nvarchar100 = "nvarchar(100)";
        public const string nvarchar150 = "nvarchar(150)";
        public const string nvarchar250 = "nvarchar(250)";
        public const string nvarchar256 = "nvarchar(256)";
        public const string nvarchar500 = "nvarchar(500)";
        public const string nvarchar1000 = "nvarchar(1000)";
        public const string nvarcharMax = "nvarchar(max)";
    }



    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.ConfigureCoreProperties(); 
            
            builder.ConfigureSoftDelete();     
            builder.Property(d => d.FirstName)
                .HasColumnName("first_name")
                .HasColumnType(DBTypes.nvarchar100)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.LastName)
                .HasColumnName("last_name")
                .HasColumnType(DBTypes.nvarchar100)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .HasColumnType(DBTypes.nvarchar256)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.PhoneNumber)
                   .HasColumnName("phone_number")
                   .HasColumnType(DBTypes.nvarchar20)
                   .HasMaxLength(20);

            builder.Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .HasColumnType(DBTypes.nvarcharMax)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .HasColumnName("role")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();
        }
    }

    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctor");
            builder.ConfigureCoreProperties(); 

            builder.Property(d => d.Specialty).HasColumnName("specialty").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100).IsRequired();

            builder.Property(d => d.SubSpecialty).HasColumnName("sub_specialty").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100).IsRequired(false);
            builder.Property(d => d.Education).HasColumnName("education").HasColumnType(DBTypes.nvarchar250).HasMaxLength(250).IsRequired(false);
            builder.Property(d => d.ExperienceYears).HasColumnName("experience_years").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(d => d.Biography).HasColumnName("biography").HasColumnType(DBTypes.nvarcharMax).IsRequired(false);

            builder.Property(d => d.Qualification).HasColumnName("qualification").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);
            builder.Property(d => d.ConsultationPrice).HasColumnName("consultation_price").HasColumnType(DBTypes.Money).IsRequired();
            builder.Property(d => d.ConsultationTime).HasColumnName("consultation_time").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(d => d.AverageReviewRate).HasColumnName("average_review_rate").HasColumnType(DBTypes.Float).HasDefaultValue(0);
            builder.Property(d => d.IsVerified).HasColumnName("is_verified").HasColumnType(DBTypes.Bit).IsRequired();

            // Relationships
            // FK to User
            builder.HasOne(d => d.User)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Clinic (One-to-Many: Clinic has many Doctors)
            builder.HasOne(d => d.Clinic)
                   .WithMany(c => c.Doctors)
                   .HasForeignKey(d => d.ClinicId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

    /// <summary>
    /// Configuration for the Patient entity.
    /// </summary>
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth").HasColumnType(DBTypes.Date).IsRequired();
            builder.Property(p => p.Gender).HasColumnName("gender").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20);
            builder.Property(p => p.Country).HasColumnName("country").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(p => p.City).HasColumnName("city").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(p => p.Street).HasColumnName("address").HasColumnType(DBTypes.nvarchar250).HasMaxLength(250);
            builder.Property(p => p.InsuranceProvider).HasColumnName("insurance_provider").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);
            builder.Property(p => p.InsurancePolicyNumber).HasColumnName("insurance_policy_number").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);

            // Relationships
            // FK to User
            builder.HasOne(p => p.User)
                   .WithOne(u => u.Patient)
                   .HasForeignKey<Patient>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the Clinic entity.
    /// </summary>
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinic");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(c => c.Name).HasColumnName("name").HasColumnType(DBTypes.nvarchar150).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Description).HasColumnName("description").HasColumnType(DBTypes.nvarchar500).HasMaxLength(500);
            builder.Property(c => c.Country).HasColumnName("country").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(c => c.City).HasColumnName("city").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(c => c.Street).HasColumnName("street").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).HasColumnName("phone_number").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("email").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);

            // Relationships
            // FK to User (Manager)
            builder.HasOne(c => c.User)
                   .WithMany(u => u.Clinics)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the Appointment entity.
    /// </summary>
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(a => a.AppointmentDate).HasColumnName("appointment_date").HasColumnType(DBTypes.Date).IsRequired();
            builder.Property(a => a.StartTime).HasColumnName("start_time").HasColumnType(DBTypes.Time).IsRequired();
            builder.Property(a => a.EndTime).HasColumnName("end_time").HasColumnType(DBTypes.Time).IsRequired();
            builder.Property(a => a.Total).HasColumnName("total").HasColumnType(DBTypes.Money).IsRequired();
            builder.Property(a => a.ConfirmationCode).HasColumnName("confirmation_code").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20).IsRequired();

            // Enum Configuration
            builder.Property(a => a.Status)
                   .HasColumnName("status")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(a => a.ConsultationType)
                   .HasColumnName("consultation_type")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(a => a.PaymentStatus)
                   .HasColumnName("payment_status")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            // Relationships
            // FK to Patient
            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Doctor
            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Clinic
            builder.HasOne(a => a.Clinic)
                   .WithMany()
                   .HasForeignKey(a => a.ClinicId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }


    /// <summary>
    /// Configuration for the DiagnosisReport entity. (NO SOFT DELETE)
    /// </summary>
    public class DiagnosisReportConfiguration : IEntityTypeConfiguration<DiagnosisReport>
    {
        public void Configure(EntityTypeBuilder<DiagnosisReport> builder)
        {
            builder.ToTable("DiagnosisReport");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(r => r.ReportTitle).HasColumnName("report_title").HasColumnType(DBTypes.nvarchar250).HasMaxLength(250).IsRequired();
            builder.Property(r => r.ReportContent).HasColumnName("report_content").HasColumnType(DBTypes.nvarcharMax).IsRequired();

            // One-to-One: Appointment can have one DiagnosisReport
            builder.HasOne(r => r.Appointment)
                   .WithOne(a => a.DiagnosisReport)
                   .HasForeignKey<DiagnosisReport>(r => r.AppointmentId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // FK to Doctor
            builder.HasOne(r => r.Doctor)
                   .WithMany(d => d.DiagnosisReports)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Patient
            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.DiagnosisReports)
                   .HasForeignKey(r => r.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the Prescription entity. (NO SOFT DELETE)
    /// </summary>
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescription");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(p => p.MedicineName).HasColumnName("medicine_name").HasColumnType(DBTypes.nvarchar150).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Dosage).HasColumnName("dosage").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Instructions).HasColumnName("instructions").HasColumnType(DBTypes.nvarchar500).HasMaxLength(500);
            builder.Property(p => p.DurationDays).HasColumnName("duration_days").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(p => p.IsActive).HasColumnName("is_active").HasColumnType(DBTypes.Bit).HasDefaultValue(true).IsRequired();

            // FK to DiagnosisReport (One-to-Many: Report has many Prescriptions)
            builder.HasOne(p => p.DiagnosisReport)
                   .WithMany(r => r.Prescriptions)
                   .HasForeignKey(p => p.DiagnosisReportId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // FK to Doctor (Doctor who issued the prescription)
            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Patient
            builder.HasOne(p => p.Patient)
                   .WithMany(pt => pt.Prescriptions)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the Transaction entity. (NO SOFT DELETE)
    /// </summary>
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(t => t.Amount).HasColumnName("amount").HasColumnType(DBTypes.Money).IsRequired();
            builder.Property(t => t.Type).HasColumnName("type").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Status).HasColumnName("status").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50).IsRequired();
            builder.Property(t => t.TransactionReference).HasColumnName("transaction_reference").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100).IsRequired();

            // FK to Patient
            builder.HasOne(t => t.Patient)
                   .WithMany(p => p.Transactions)
                   .HasForeignKey(t => t.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Appointment
            builder.HasOne(t => t.Appointment)
                   .WithMany()
                   .HasForeignKey(t => t.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the PatientNotes entity. (NO SOFT DELETE)
    /// </summary>
    public class PatientNotesConfiguration : IEntityTypeConfiguration<PatientNotes>
    {
        public void Configure(EntityTypeBuilder<PatientNotes> builder)
        {
            builder.ToTable("PatientNotes");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            // NoteType is now an enum, configured to be stored as a string name in the DB
            builder.Property(n => n.NoteType)
                   .HasColumnName("note_type")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(n => n.NoteContent).HasColumnName("note_content").HasColumnType(DBTypes.nvarcharMax).IsRequired();

            // FK to Patient
            builder.HasOne(n => n.Patient)
                   .WithMany(p => p.PatientNotes)
                   .HasForeignKey(n => n.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Appointment
            builder.HasOne(n => n.Appointment)
                   .WithMany(a => a.PatientNotes)
                   .HasForeignKey(n => n.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }


    /// <summary>
    /// Configuration for the NotificationSchedule entity. (NO SOFT DELETE)
    /// </summary>
    public class NotificationScheduleConfiguration : IEntityTypeConfiguration<NotificationSchedule>
    {
        public void Configure(EntityTypeBuilder<NotificationSchedule> builder)
        {
            builder.ToTable("NotificationSchedule");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(ns => ns.Frequency).HasColumnName("frequency").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(ns => ns.NextRunDate).HasColumnName("next_run_date").HasColumnType(DBTypes.DateTime2).IsRequired();
            builder.Property(ns => ns.IsActive).HasColumnName("is_active").HasColumnType(DBTypes.Bit).HasDefaultValue(true).IsRequired();
            builder.Property(ns => ns.IsDelivered).HasColumnName("is_delivered").HasColumnType(DBTypes.Bit).HasDefaultValue(false).IsRequired();

            // Enum Configuration for RecurrenceType
            builder.Property(ns => ns.RecurrenceType)
                   .HasColumnName("recurrence_type")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            // Enum Configuration for NotificationType
            builder.Property(ns => ns.NotificationType)
                   .HasColumnName("notification_type")
                   .HasColumnType(DBTypes.nvarchar100)
                   .HasMaxLength(100)
                   .HasConversion<string>()
                   .IsRequired();

            // FK to Patient
            builder.HasOne(ns => ns.Patient)
                   .WithMany(p => p.NotificationSchedules)
                   .HasForeignKey(ns => ns.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Appointment
            builder.HasOne(ns => ns.Appointment)
                   .WithMany(a => a.NotificationSchedules)
                   .HasForeignKey(ns => ns.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }


    // --- Auxiliary Tables (Existing) ---

    /// <summary>
    /// Configuration for the DoctorSchedule entity.
    /// </summary>
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("DoctorSchedule");
            builder.ConfigureCoreProperties(); // Id, CreatedAt
            builder.ConfigureSoftDelete();     // IsDeleted

            builder.Property(ds => ds.DayOfWeek).HasColumnName("day_of_week").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20).IsRequired();
            builder.Property(ds => ds.StartTime).HasColumnName("start_time").HasColumnType(DBTypes.Time).IsRequired();
            builder.Property(ds => ds.EndTime).HasColumnName("end_time").HasColumnType(DBTypes.Time).IsRequired();

            // Composite unique index for scheduling conflict prevention
            builder.HasIndex(ds => new { ds.DoctorId, ds.DayOfWeek, ds.StartTime, ds.ClinicId }).IsUnique();

            // Relationships
            // FK to Doctor
            builder.HasOne(ds => ds.Doctor)
                   .WithMany(d => d.DoctorSchedules)
                   .HasForeignKey(ds => ds.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            // FK to Clinic
            builder.HasOne(ds => ds.Clinic)
                   .WithMany(c => c.DoctorSchedules)
                   .HasForeignKey(ds => ds.ClinicId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }

    /// <summary>
    /// Configuration for the Review entity.
    /// </summary>
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(r => r.Rating).HasColumnName("rating").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(r => r.Comment).HasColumnName("comment").HasColumnType(DBTypes.nvarchar1000).HasMaxLength(1000);

            // Relationships
            // FK to Doctor (Doctor receives the review)
            builder.HasOne(r => r.Doctor)
                   .WithMany(d => d.Reviews)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Patient (Patient wrote the review)
            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // Ensure a patient can only review a doctor once (or per appointment, but simplifying to once per doctor for now)
            builder.HasIndex(r => new { r.PatientId, r.DoctorId }).IsUnique();
        }
    }

}
