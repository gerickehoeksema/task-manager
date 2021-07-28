using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Member.Query
{
    /// <summary>
    /// Get active members
    /// </summary>
    public class GetMemberListQuery : IRequest<IList<MemberDTO>>
    {
        public class GetMemberListQueryHandler : IRequestHandler<GetMemberListQuery, IList<MemberDTO>>
        {
            internal IApplicationDbContext context;
            internal IMapper mapper;
            internal IIdentityUserService identityUserService;

            public GetMemberListQueryHandler(IApplicationDbContext context
                , IMapper mapper
                , IIdentityUserService identityUserService)
            {
                this.context = context;
                this.mapper = mapper;
                this.identityUserService = identityUserService;
            }

            public async Task<IList<MemberDTO>> Handle(GetMemberListQuery request, CancellationToken cancellationToken)
            {
                var members = await context.Members
                    .Where(m => m.IsActive)
                    .Include(m => m.Tasks)
                    .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                for (int i = 0; i < members.Count; i++)
                {
                    var user = await identityUserService.FindUserAsync(members[i].UserId).ConfigureAwait(false);
                    members[i].Name = user.name;
                    members[i].Surname = user.surname;
                }

                return members;
            }
        }
    }
}
