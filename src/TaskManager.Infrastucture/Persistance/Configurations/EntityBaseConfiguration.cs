using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Common;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    /// <summary>
    /// Configure all entities that has base class <c>AuditableEntity</c>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UpdatedDate).IsRequired(false);
            builder.Property(p => p.UpdatedBy).IsRequired(false);

            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}
