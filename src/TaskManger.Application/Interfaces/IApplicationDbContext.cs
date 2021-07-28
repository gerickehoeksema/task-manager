using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Enitities.Member> Members { get; set; }
        DbSet<Domain.Enitities.Task> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
