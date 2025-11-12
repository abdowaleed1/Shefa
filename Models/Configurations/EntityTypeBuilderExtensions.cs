using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using Models.InterFaces;

namespace Models.Configurations
{
    public static class EntityTypeBuilderExtensions
    {
        public static void ConfigureCoreProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .HasColumnType(DBTypes.Int)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType(DBTypes.DateTime2)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
        }

        public static void ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, ISoftDelete
        {
            // IsDeleted (Soft Delete Field)
            builder.Property(e => e.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasColumnType(DBTypes.Bit)
                   .HasDefaultValue(false)
                   .IsRequired();
        }
    }

}
