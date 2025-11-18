using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.ConfigureCoreProperties(); 

            builder.Property(r => r.DoctorRating)
                .HasColumnName("doctor_rating")
                .HasColumnType(DBTypes.Int)
                .IsRequired();
            builder.Property(r => r.DoctorComment)
                .HasColumnName("doctor_comment")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(1000)
                .IsRequired(false);
            builder.Property(r => r.ClinicRating)
                .HasColumnName("clinic_rating")
                .HasColumnType(DBTypes.Int)
                .IsRequired();
            builder.Property(r => r.ClinicComment)
                .HasColumnName("clinic_comment")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.HasOne(r => r.Doctor)
                   .WithMany(d => d.Reviews)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.Reviews)
                   .HasForeignKey(r => r.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasIndex(r => new { r.PatientId, r.DoctorId }).IsUnique();
            
            builder.HasOne(r => r.Clinic)
                   .WithMany(c => c.Reviews)
                   .HasForeignKey(r => r.ClinicId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();


        }
    }

}
