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

            builder.Property(d => d.Specialty)
                .HasColumnName("specialty")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.Education)
                .HasColumnName("education")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(d => d.ExperienceYears)
                .HasColumnName("experience_years")
                .HasColumnType(DBTypes.Int)
                .IsRequired(false);

            builder.Property(d => d.Biography)
                .HasColumnName("biography")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(d => d.ConsultationPrice)
                .HasColumnName("consultation_price")
                .HasColumnType(DBTypes.Money)
                .IsRequired();
            
            builder.Property(d => d.ConsultationTime)
                .HasColumnName("consultation_time")
                .HasColumnType(DBTypes.Int)
                .IsRequired();
            
            builder.Property(d => d.AverageReviewRate)
                .HasColumnName("average_review_rate")
                .HasColumnType(DBTypes.Decimal18_2)
                .HasDefaultValue(0);
            
            builder.Property(d => d.CountOfReviews)
                .HasColumnName("Count_of_reviews")
                .HasColumnType(DBTypes.Int)
                .HasDefaultValue(0);
            
            builder.Property(d => d.IsVerified)
                .HasColumnName("is_verified")
                .HasColumnType(DBTypes.Bit)
                .IsRequired();

            builder.Property(c => c.ClinicId)
                   .HasColumnName("clinic_id");

            builder.HasOne(d => d.Clinic)
                   .WithMany(c => c.Doctors)
                   .HasForeignKey(d => d.ClinicId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.UserId)
               .HasColumnName("user_id");

            builder.HasOne(D => D.User)
              .WithOne(u=> u.Doctor)
              .HasForeignKey<Doctor>(n => n.UserId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();

        }
    }

}
