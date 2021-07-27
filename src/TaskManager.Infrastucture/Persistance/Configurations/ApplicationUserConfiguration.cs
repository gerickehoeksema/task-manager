using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    /// <summary>
    /// <c>ApplicationUser</c> Identity entity configuration
    /// </summary>
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(p => p.Id).HasColumnName("UserId");

            builder.ToTable("Users", "security");
        }
    }
}
