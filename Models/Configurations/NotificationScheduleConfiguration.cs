using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class NotificationScheduleConfiguration : IEntityTypeConfiguration<NotificationSchedule>
    {
        public void Configure(EntityTypeBuilder<NotificationSchedule> builder)
        {
            builder.ToTable("NotificationSchedule");
            builder.ConfigureCoreProperties(); 
            builder.Property(ns => ns.Frequency).HasColumnName("frequency").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(ns => ns.NextRunDate).HasColumnName("next_run_date").HasColumnType(DBTypes.DateTime2).IsRequired();
            builder.Property(ns => ns.IsActive).HasColumnName("is_active").HasColumnType(DBTypes.Bit).HasDefaultValue(true).IsRequired();
            builder.Property(ns => ns.IsDelivered).HasColumnName("is_delivered").HasColumnType(DBTypes.Bit).HasDefaultValue(false).IsRequired();
            builder.Property(n => n.MedicationName).HasColumnName("medication_name").HasColumnType(DBTypes.nvarchar500).HasMaxLength(500).IsRequired();

            builder.Property(ns => ns.RecurrenceType)
                   .HasColumnName("recurrence_type")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(ns => ns.Patient)
                   .WithMany(p => p.NotificationSchedules)
                   .HasForeignKey(ns => ns.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

        }
    }

}
