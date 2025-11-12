using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");
            builder.ConfigureCoreProperties(); 

            builder.Property(a => a.AppointmentDate).HasColumnName("appointment_date").HasColumnType(DBTypes.Date).IsRequired();
            builder.Property(a => a.ConfirmationCode).HasColumnName("confirmation_code").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20).IsRequired();

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

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

        }
    }

}
