using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Assign Task to Member
    /// </summary>
    public class AssignTaskCommand : IRequest<bool>
    {
        public long TaskId { get; set; }
        public long MemberId { get; set; }

        public class AssignTaskCommandHandler : IRequestHandler<AssignTaskCommand, bool>
        {
            internal IApplicationDbContext context;

            public AssignTaskCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await context.Tasks
                    .FirstOrDefaultAsync(t => t.Id == request.TaskId && t.IsActive, cancellationToken)
                    .ConfigureAwait(false);

                if (task == null)
                    return false;

                var member = await context.Members
                    .FirstOrDefaultAsync(m => m.Id == request.MemberId && m.IsActive, cancellationToken)
                    .ConfigureAwait(false);

                if (member == null)
                    return false;

                task.AssignedTo = member.Id;

                return (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;
            }
        }
    }
}
