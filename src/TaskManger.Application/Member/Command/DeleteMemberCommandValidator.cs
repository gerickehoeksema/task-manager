using FluentValidation;

namespace TaskManager.Application.Member.Command
{
    /// <summary>
    /// Validator for <see cref="DeleteMemberCommand"/>
    /// </summary>
    public class DeleteMemberCommandValidator : AbstractValidator<DeleteMemberCommand>
    {
        public DeleteMemberCommandValidator()
        {
            RuleFor(p => p.MemberId).GreaterThan(0).WithMessage("Valid member id is required");
        }
    }
}
