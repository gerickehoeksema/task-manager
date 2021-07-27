using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enitities;

namespace TaskManager.Infrastucture.Persistance.Configurations
{
    public class MemberConfiguration : EntityBaseConfiguration<Member>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Member> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Surname).IsRequired();

            builder
                .HasMany(p => p.Tasks)
                .WithOne(p => p.Member);

            builder.ToTable("Member", "member");
        }
    }
}
