using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Member.Command
{
    /// <summary>
    /// Delete a member
    /// </summary>
    public class DeleteMemberCommand : IRequest<bool>
    {
        public long MemberId { get; set; }

        public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
        {
            internal IApplicationDbContext context;
            internal IIdentityUserService identityUserService;

            public DeleteMemberCommandHandler(IApplicationDbContext context,
                IIdentityUserService identityUserService)
            {
                this.context = context;
                this.identityUserService = identityUserService;
            }
            public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
            {
                var member = await context.Members
                    .FirstOrDefaultAsync(m => m.Id == request.MemberId, cancellationToken)
                    .ConfigureAwait(false);

                if (member == null)
                    return false;

                member.IsActive = false;

                var result = await identityUserService.DeleteUserAsync(member.UserId).ConfigureAwait(false);
                if (!result.Succeeded)
                    return false;

                return (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false)) > 0;
            }
        }
    }
}
