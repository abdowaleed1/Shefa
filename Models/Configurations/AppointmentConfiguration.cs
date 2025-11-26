using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entities;
using Models.Enums;

namespace Models.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");
            builder.ConfigureCoreProperties(); 

            builder.Property(a => a.AppointmentDate)
                .HasColumnName("appointment_date")
                .HasColumnType(DBTypes.Date)
                .IsRequired();
            builder.Property(a => a.ConfirmationCode)
                .HasColumnName("confirmation_code")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(e => e.PatientId)
                   .HasColumnName("patient_id");

            builder.Property(e => e.DoctorId)
                   .HasColumnName("doctor_id");

            builder.Property(a => a.AppointmentStatus)
                   .HasColumnName("status")
                   .HasColumnType(DBTypes.NvarChar)
                   .HasMaxLength(50)
                   .HasConversion(new EnumToStringConverter<AppointmentStatus>())
                   .IsRequired();

            builder.Property(a => a.ConsultationType)
                   .HasColumnName("consultation_type")
                   .HasColumnType(DBTypes.NvarChar)
                   .HasMaxLength(50)
                   .HasConversion(new EnumToStringConverter<ConsultationType>())
                   .IsRequired();

            builder.Property(a => a.PaymentStatus)
                   .HasColumnName("payment_status")
                   .HasColumnType(DBTypes.NvarChar)
                   .HasMaxLength(50)
                   .HasConversion(new EnumToStringConverter<PaymentStatus>())
                   .IsRequired();

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

        }
    }

}
