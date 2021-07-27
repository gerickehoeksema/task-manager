using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    public class TaskConfiguration : EntityBaseConfiguration<Domain.Enitities.Task>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Domain.Enitities.Task> builder)
        {
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Status).IsRequired();

            builder.ToTable("Task", "task");
        }
    }
}
