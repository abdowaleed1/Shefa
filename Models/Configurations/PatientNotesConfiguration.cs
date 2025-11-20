using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class PatientNotesConfiguration : IEntityTypeConfiguration<PatientNotes>
    {
        public void Configure(EntityTypeBuilder<PatientNotes> builder)
        {
            builder.ToTable("PatientNotes");
            builder.ConfigureCoreProperties(); 


            builder.Property(n => n.NoteType)
                   .HasColumnName("note_type")
                   .HasColumnType(DBTypes.NvarChar)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(n => n.NoteContent)
                .HasColumnName("note_content")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(c => c.PatientId)
               .HasColumnName("patient_id")
               .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.Property(c => c.AppointmentId)
               .HasColumnName("appointment_id")
               .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.HasOne(n => n.Patient)
                   .WithMany(p => p.PatientNotes)
                   .HasForeignKey(n => n.PatientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();


            builder.HasOne(n => n.Appointment)
                   .WithMany(a => a.PatientNotes)
                   .HasForeignKey(n => n.AppointmentId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }

}
