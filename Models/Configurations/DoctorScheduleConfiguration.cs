using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

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
                    .HasMaxLength(200)
                    .HasConversion<string>()
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
                   .HasColumnName("doctor_id")
                   .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.Property(c => c.ClinicId)
                   .HasColumnName("clinic_id")
                   .HasColumnType(DBTypes.UniQueIdEntifier);


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
