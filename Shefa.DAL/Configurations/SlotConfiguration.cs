using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable("Slots");
            builder.ConfigureCoreProperties();

            builder.Property(x => x.DoctorId)
                .IsRequired();

            builder.HasOne(x => x.Doctor)
                .WithMany(d => d.Slots)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.ClinicId)
                .IsRequired();

            builder.HasOne(x => x.Clinic)
                .WithMany(c => c.Slots)
                .HasForeignKey(x => x.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.Date)
                .HasColumnName("date")
                .HasColumnType(DBTypes.date)
                .IsRequired();
            builder.Property(x => x.StartTime)
                .HasColumnName("start_time")
                .HasColumnType(DBTypes.Time)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnName("end_time")
                .HasColumnType(DBTypes.Time)
                .IsRequired();

            builder.Property(x => x.IsBooked)
                .HasColumnName("is_booked")
                .HasColumnType(DBTypes.Bit)
                .HasDefaultValue(false);

            builder.Property(x => x.IsBlocked)
                .HasColumnName("is_blocked")
                .HasColumnType(DBTypes.Bit)
                .HasDefaultValue(false);

            builder.HasIndex(x => new { x.DoctorId, x.Date, x.StartTime })
                .IsUnique();

        }

    }
}
