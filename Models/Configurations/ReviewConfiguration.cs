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
            builder.ConfigureCoreProperties(); // Id, CreatedAt

            builder.Property(r => r.DoctorRating).HasColumnName("doctor_rating").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(r => r.DoctorComment).HasColumnName("doctor_comment").HasColumnType(DBTypes.nvarchar1000).HasMaxLength(1000);
            builder.Property(r => r.ClinicRating).HasColumnName("clinic_rating").HasColumnType(DBTypes.Int).IsRequired();
            builder.Property(r => r.ClinicComment).HasColumnName("clinic_comment").HasColumnType(DBTypes.nvarchar1000).HasMaxLength(1000);

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
