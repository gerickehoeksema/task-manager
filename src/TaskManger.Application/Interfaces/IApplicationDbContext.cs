using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Domain.Enitities;

namespace TaskManager.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Member> Members { get; set; }
        DbSet<Domain.Enitities.Task> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
