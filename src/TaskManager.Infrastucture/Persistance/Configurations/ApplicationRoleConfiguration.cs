using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    /// <summary>
    /// <c>ApplicationRole</c> Identity entity configuration
    /// </summary>
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(p => p.Id).HasColumnName("RoleId");

            builder.ToTable("Roles", "security");
        }
    }
}
