using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Create new <see cref="Domain.Enitities.Task"/>
    /// </summary>
    public class CreateTaskCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus_Enum Status { get; set; }

        public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
        {
            internal readonly IApplicationDbContext context;

            public CreateTaskCommandHandler(IApplicationDbContext context)
            {
                this.context = context;
            }

            public async Task<bool> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                await context.Tasks
                    .AddAsync(new Domain.Enitities.Task
                    {
                        Title = request.Title,
                        Description = request.Description,
                        Status = request.Status
                    }, cancellationToken)
                    .ConfigureAwait(false);

                return (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;
            }
        }
    }
}
