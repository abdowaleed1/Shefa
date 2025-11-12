using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescription");
            builder.ConfigureCoreProperties(); // Id, CreatedAt
            
            builder.Property(d => d.PrescriptionImageURL)
               .HasColumnName("prescription_image_url")
               .HasColumnType(DBTypes.nvarcharMax)
               .IsRequired();



            // FK to Doctor (Doctor who issued the prescription)
            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            // FK to Patient
            builder.HasOne(p => p.Patient)
                   .WithMany(pt => pt.Prescriptions)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

}
