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

            builder.Property(c => c.Name).HasColumnName("name").HasColumnType(DBTypes.nvarchar150).HasMaxLength(150).IsRequired();
            builder.Property(c => c.Description).HasColumnName("description").HasColumnType(DBTypes.nvarchar500).HasMaxLength(500);
            builder.Property(c => c.Country).HasColumnName("country").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(c => c.City).HasColumnName("city").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(c => c.Street).HasColumnName("street").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).HasColumnName("phone_number").HasColumnType(DBTypes.nvarchar20).HasMaxLength(20);
            builder.Property(c => c.Email).HasColumnName("email").HasColumnType(DBTypes.nvarchar100).HasMaxLength(100);

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Clinics)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
            builder.HasMany(c => c.Reviews)
               .WithOne(r => r.Clinic)
               .HasForeignKey(r => r.ClinicId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
        }
    }

}
