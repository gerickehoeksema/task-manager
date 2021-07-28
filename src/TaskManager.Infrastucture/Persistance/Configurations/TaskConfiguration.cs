using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enitities;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    public class TaskConfiguration : EntityBaseConfiguration<Task>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Task> builder)
        {
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.HasOne(p => p.Member)
                .WithMany(p => p.Tasks);

            builder.ToTable("Task", "task");
        }
    }
}
