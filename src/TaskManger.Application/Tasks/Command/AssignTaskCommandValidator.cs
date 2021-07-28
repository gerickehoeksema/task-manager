using FluentValidation;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Validator for <see cref="AssignTaskCommand"/>
    /// </summary>
    public class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
    {
        public AssignTaskCommandValidator()
        {
            RuleFor(p => p.TaskId).GreaterThan(0).WithMessage("Valid task id is required");
            RuleFor(p => p.MemberId).GreaterThan(0).WithMessage("Valid member id is required");
        }
    }
}
