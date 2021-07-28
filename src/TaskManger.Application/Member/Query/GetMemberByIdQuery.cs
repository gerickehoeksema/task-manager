using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Member.Query
{
    /// <summary>
    /// Get Member by id
    /// </summary>
    public class GetMemberByIdQuery : IRequest<MemberDTO>
    {
        public long MemberId { get; set; }

        public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDTO>
        {
            internal IApplicationDbContext context;
            internal IMapper mapper;
            internal IIdentityUserService identityUserService;

            public GetMemberByIdQueryHandler(IApplicationDbContext context
                , IMapper mapper
                , IIdentityUserService identityUserService)
            {
                this.context = context;
                this.mapper = mapper;
                this.identityUserService = identityUserService;
            }

            public async Task<MemberDTO> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
            {
                var member = await context.Members
                    .Where(m => m.IsActive)
                    .Include(m => m.Tasks)
                    .ProjectTo<MemberDTO>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (member == null)
                    return null;

                var user = await identityUserService.FindUserAsync(member.UserId).ConfigureAwait(false);
                member.Name = user.name;
                member.Surname = user.surname;

                return member;
            }
        }
    }
}
