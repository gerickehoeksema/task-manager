using FluentValidation;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Validator for <see cref="UpdateTaskCommand"/>
    /// </summary>
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(p => p.TaskId).GreaterThan(0).WithMessage("Valid task id is required");
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");

            // TODO: Validate if task is in db
        }
    }
}
