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
               .HasColumnType(DBTypes.nvarchar100)
               .HasMaxLength(100)
               .IsRequired();
            builder.Property(d => d.ReportURL)
               .HasColumnName("report_url")
               .HasColumnType(DBTypes.nvarcharMax)
               .IsRequired();


            builder.HasOne(r => r.Doctor)
                   .WithMany(d => d.DiagnosisReports)
                   .HasForeignKey(r => r.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(r => r.Patient)
                   .WithMany(p => p.DiagnosisReports)
                   .HasForeignKey(r => r.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

}
