using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Common;
using TaskManager.Domain.Enitities;
using TaskManager.Infrastucture.Identity;

namespace TaskManager.Infrastucture.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>, IApplicationDbContext
    {
        private readonly IDateTimeService dateTime;

        public ApplicationDbContext(DbContextOptions options, IDateTimeService dateTime)
            : base(options)
        {
            this.dateTime = dateTime;
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Domain.Enitities.Task> Tasks { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = dateTime.Now;
                        break;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
