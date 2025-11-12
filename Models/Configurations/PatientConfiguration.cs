using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patient");
            builder.ConfigureCoreProperties(); 

            builder.Property(p => p.DateOfBirth).HasColumnName("date_of_birth").HasColumnType(DBTypes.Date).IsRequired();
            builder.Property(p => p.Gender).HasColumnName("gender").HasConversion<string>().HasColumnType(DBTypes.nvarchar20).HasMaxLength(20);
            builder.Property(p => p.Country).HasColumnName("country").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(p => p.City).HasColumnName("city").HasColumnType(DBTypes.nvarchar50).HasMaxLength(50);
            builder.Property(p => p.Street).HasColumnName("address").HasColumnType(DBTypes.nvarchar250).HasMaxLength(250);


            builder.HasOne(p => p.User)
                   .WithOne(u => u.Patient)
                   .HasForeignKey<Patient>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }

}
