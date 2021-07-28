using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Delete a task
    /// </summary>
    public class DeleteTaskCommand : IRequest<bool>
    {
        public long TaskId { get; set; }

        public class DeleteTaskCommandhandler : IRequestHandler<DeleteTaskCommand, bool>
        {
            internal IApplicationDbContext context;

            public DeleteTaskCommandhandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await context.Tasks
                    .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken)
                    .ConfigureAwait(false);

                if (task == null)
                    return false;

                task.IsActive = false;
                task.Status = Domain.Enums.TaskStatus_Enum.Deleted;

                return (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;
            }
        }
    }
}
