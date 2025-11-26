using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entities;
using Models.Enums;

namespace Models.Configurations
{
    public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
    {
        public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
        {
            builder.ToTable("DoctorSchedule");
            builder.ConfigureCoreProperties(); 

            builder.Property(ds => ds.DayOfWeek)
                    .HasColumnName("day_of_week")
                    .HasColumnType(DBTypes.NvarChar)
                    .HasMaxLength(300)
                    .HasConversion(new EnumToStringConverter<DayOfWeek>())
                    .IsRequired();
            builder.Property(ds => ds.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType(DBTypes.DateTime)
                    .IsRequired();
            builder.Property(ds => ds.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType(DBTypes.DateTime)
                    .IsRequired();
            builder.Property(c => c.DoctorId)
                   .HasColumnName("doctor_id");

            builder.Property(c => c.ClinicId)
                   .HasColumnName("clinic_id");


            builder.HasIndex(ds => new { ds.DoctorId, ds.DayOfWeek, ds.StartTime, ds.ClinicId })
                .IsUnique();


            builder.HasOne(ds => ds.Doctor)
                   .WithMany(d => d.DoctorSchedules)
                   .HasForeignKey(ds => ds.DoctorId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(ds => ds.Clinic)
                   .WithMany(c => c.DoctorSchedules)
                   .HasForeignKey(ds => ds.ClinicId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }

}
