using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class DiagnosisReportConfiguration : IEntityTypeConfiguration<DiagnosisReport>
    {
        public void Configure(EntityTypeBuilder<DiagnosisReport> builder)
        {
            builder.ToTable("DiagnosisReport");
            builder.ConfigureCoreProperties();

            builder.Property(d => d.ReportType)
               .HasColumnName("report_type")
               .HasColumnType(DBTypes.NvarChar)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(d => d.ReportURL)
               .HasColumnName("report_url")
               .HasColumnType(DBTypes.NvarCharMax)
               .IsRequired();

            builder.Property(c => c.DoctorId)
                   .HasColumnName("doctor_id")
                   .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.Property(c => c.PatientId)
                   .HasColumnName("patient_id")
                   .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.HasOne(dr => dr.Doctor)
                   .WithMany(d => d.DiagnosisReports)
                   .HasForeignKey(dr => dr.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();

            builder.HasOne(dr => dr.Patient)
                   .WithMany(p => p.DiagnosisReports)
                   .HasForeignKey(dr => dr.PatientId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }

}
