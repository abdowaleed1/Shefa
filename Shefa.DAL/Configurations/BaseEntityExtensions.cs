using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using Models.InterFaces;


namespace Models.Configurations
{
    public static class BaseEntityExtensions
    {
        public static void ConfigureCoreProperties<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id");

            builder.Property(e => e.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType(DBTypes.DateTime2)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(e => e.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasColumnType(DBTypes.Bit)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(e => e.IsActive)
                   .HasColumnName("is_active")
                   .HasColumnType(DBTypes.Bit)
                   .HasDefaultValue(true)
                   .IsRequired();


            builder.Property(d => d.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType(DBTypes.DateTime2)
                    .IsRequired(false);

        }

        //public static void ConfigureSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, ISoftDelete
        //{
        //}
    }

}
