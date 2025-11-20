using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinic");
            builder.ConfigureCoreProperties(); 

            builder.Property(c => c.Name)
                .HasColumnName("name")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Description)
                    .HasColumnName("description")
                    .HasColumnType(DBTypes.NvarChar)
                    .HasMaxLength(500);

            builder.Property(c => c.Country)
                .HasColumnName("country")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50);

            builder.Property(c => c.City)
                .HasColumnName("city")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(50);

            builder.Property(c => c.Street)
                .HasColumnName("street")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(100);

            builder.Property(c => c.PhoneNumber)
                .HasColumnName("phone_number")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .HasColumnName("email")
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(100);

            builder.Property(c => c.ManagerId)
                   .HasColumnName("manager_id")
                   .HasColumnType(DBTypes.UniQueIdEntifier);

            builder.HasOne(c => c.Manager)
                   .WithMany(u => u.Clinics)
                   .HasForeignKey(c => c.ManagerId)
                   .OnDelete(DeleteBehavior.NoAction)
                   .IsRequired();
        }
    }

}
