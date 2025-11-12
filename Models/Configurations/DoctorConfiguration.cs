using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctor");
            builder.ConfigureCoreProperties(); 

            builder.Property(d => d.Specialty).HasColumnName("specialty").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100).IsRequired();

            builder.Property(d => d.Education).HasColumnName("education").HasColumnType(DBTypes.nvarchar250).HasMaxLength(250).IsRequired(false);
            builder.Property(d => d.ExperienceYears).HasColumnName("experience_years").HasColumnType(DBTypes.Int).IsRequired(false);
            builder.Property(d => d.Biography).HasColumnName("biography").HasColumnType(DBTypes.nvarcharMax).IsRequired(false);

            builder.Property(d => d.ConsultationPrice).HasColumnName("consultation_price").HasColumnType(DBTypes.Money).IsRequired();
            builder.Property(d => d.ConsultationTime).HasColumnName("consultation_time").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(d => d.AverageReviewRate).HasColumnName("average_review_rate").HasColumnType(DBTypes.Float).HasDefaultValue(0);
            builder.Property(d => d.IsVerified).HasColumnName("is_verified").HasColumnType(DBTypes.Bit).IsRequired();

            builder.HasOne(d => d.User)
                   .WithOne(u => u.Doctor)
                   .HasForeignKey<Doctor>(d => d.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(d => d.Clinic)
                   .WithMany(c => c.Doctors)
                   .HasForeignKey(d => d.ClinicId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
