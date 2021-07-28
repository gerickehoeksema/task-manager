using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Update a <see cref="Domain.Enitities.Task"/>
    /// </summary>
    public class UpdateTaskCommand : IRequest<bool>
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus_Enum Status { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndDate { get; set; }
        public long? AssignedTo { get; set; }

        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
        {
            internal IApplicationDbContext context;

            public UpdateTaskCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await context.Tasks
                    .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken)
                    .ConfigureAwait(false);

                // TODO: Handle not found better

                if (task == null)
                    return false;

                task.Title = request.Title;
                task.Description = request.Description;
                task.Status = request.Status;
                task.StartTime = request.StartTime;
                task.EndDate = request.EndDate;
                task.MemberId = request.AssignedTo; // TODO: Check if member is in db

                return (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;
            }
        }
    }
}
