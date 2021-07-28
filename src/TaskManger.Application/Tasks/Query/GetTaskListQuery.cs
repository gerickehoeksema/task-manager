using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Tasks.Query
{
    /// <summary>
    /// Get active Tasks
    /// </summary>
    public class GetTaskListQuery : IRequest<IList<TaskDTO>>
    {
        public class GetTaskListQueryHandler : IRequestHandler<GetTaskListQuery, IList<TaskDTO>>
        {
            internal IApplicationDbContext context;
            internal IMapper mapper;

            public GetTaskListQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                this.context = context;
                this.mapper = mapper;
            }

            public async Task<IList<TaskDTO>> Handle(GetTaskListQuery request, CancellationToken cancellationToken)
            {
                var tasks = await context.Tasks
                    .Where(t => t.IsActive)
                    .ProjectTo<TaskDTO>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                return tasks;
            }
        }
    }
}
