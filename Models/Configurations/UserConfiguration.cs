using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            
            builder.ConfigureCoreProperties(); 
            
            builder.ConfigureSoftDelete();     
            builder.Property(d => d.FirstName)
                .HasColumnName("first_name")
                .HasColumnType(DBTypes.nvarchar100)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.LastName)
                .HasColumnName("last_name")
                .HasColumnType(DBTypes.nvarchar100)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(d => d.UpatedAt)
            .HasColumnName("upated_at")
            .HasColumnType(DBTypes.DateTime2)
            .IsRequired(false);

            builder.Property(u => u.Email)
                   .HasColumnName("email")
                   .HasColumnType(DBTypes.nvarchar256)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.Property(u => u.PhoneNumber)
                   .HasColumnName("phone_number")
                   .HasColumnType(DBTypes.nvarchar20)
                   .HasMaxLength(20);

            builder.Property(u => u.PasswordHash)
                   .HasColumnName("password_hash")
                   .HasColumnType(DBTypes.nvarcharMax)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .HasColumnName("role")
                   .HasColumnType(DBTypes.nvarchar50)
                   .HasMaxLength(50)
                   .HasConversion<string>()
                   .IsRequired();
        }
    }

}
