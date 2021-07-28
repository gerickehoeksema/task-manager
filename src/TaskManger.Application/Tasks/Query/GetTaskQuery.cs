using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Tasks.Query
{
    public class GetTaskQuery : IRequest<TaskDTO>
    {
        public long TaskId { get; set; }

        public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDTO>
        {
            internal IApplicationDbContext context;
            internal IMapper mapper;

            public GetTaskQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<TaskDTO> Handle(GetTaskQuery request, CancellationToken cancellationToken)
            {
                var task = await context.Tasks
                    .FirstOrDefaultAsync(t => t.Id == request.TaskId, cancellationToken)
                    .ConfigureAwait(false);

                if (task == null)
                    return null;

                return mapper.Map<TaskDTO>(task);
            }
        }
    }
}
