using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");
            builder.ConfigureCoreProperties(); 

            builder.Property(t => t.Amount)
                .HasColumnName("amount")
                .HasColumnType(DBTypes.Money)
                .IsRequired();

            builder.Property(t => t.Type)
                .HasColumnName("type")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.Status)
                .HasColumnName("status")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(t => t.TransactionReference)
                .HasColumnName("transaction_reference")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(c => c.PatientId)
               .HasColumnName("patient_id")
               .HasColumnType(DBTypes.Int);

            builder.Property(c => c.AppointmentId)
                   .HasColumnName("appointment_id")
                   .HasColumnType(DBTypes.Int);


            builder.HasOne(t => t.Patient)
                   .WithMany(p => p.Transactions)
                   .HasForeignKey(t => t.PatientId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            
            builder.HasOne(t => t.Appointment)
                   .WithMany()
                   .HasForeignKey(t => t.AppointmentId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }

}
