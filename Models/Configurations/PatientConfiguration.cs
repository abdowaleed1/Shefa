using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Entities;
using Models.Enums;

namespace Models.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.ConfigureCoreProperties();

            builder.Property(p => p.DateOfBirth)
                .HasColumnName("date_of_birth")
                .HasColumnType(DBTypes.Date)
                .IsRequired();
            
            builder.Property(p => p.Gender)
                .HasColumnName("gender")
                .HasConversion(new EnumToStringConverter<Gender>())
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(20);

            builder.Property(p => p.Country)
                .HasColumnName("country")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50);

            builder.Property(p => p.City)
                .HasColumnName("city")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50);

            builder.Property(p => p.Street)
                .HasColumnName("address")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(250);
            builder.Property(c => c.UserId)
               .HasColumnName("user_id");

            builder.HasOne(n => n.User)
              .WithOne(u=>u.Patient)
              .HasForeignKey<Patient>(n => n.UserId)
              .OnDelete(DeleteBehavior.Cascade)
              .IsRequired();


        }
    }

}
