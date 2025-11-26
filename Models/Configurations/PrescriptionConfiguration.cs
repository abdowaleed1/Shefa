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
            builder.ConfigureCoreProperties(); 
            
            builder.Property(d => d.PrescriptionImageURL)
               .HasColumnName("prescription_image_url")
               .HasColumnType(DBTypes.NvarCharMax)
               .IsRequired();

            builder.Property(c => c.DoctorId)
               .HasColumnName("doctor_id");

            builder.Property(c => c.PatientId)
                   .HasColumnName("patient_id");

            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.Prescriptions)
                   .HasForeignKey(p => p.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

            builder.HasOne(p => p.Patient)
                   .WithMany(pt => pt.Prescriptions)
                   .HasForeignKey(p => p.PatientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }

}
