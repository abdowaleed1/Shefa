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
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(n => n.NoteContent).HasColumnName("note_content").HasColumnType(DBTypes.nvarcharMax).IsRequired();

            builder.HasOne(n => n.Patient)
                   .WithMany(p => p.PatientNotes)
                   .HasForeignKey(n => n.PatientId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();


            builder.HasOne(n => n.Appointment)
                   .WithMany(a => a.PatientNotes)
                   .HasForeignKey(n => n.AppointmentId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

}
