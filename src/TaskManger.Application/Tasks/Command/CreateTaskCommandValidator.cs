using FluentValidation;

namespace TaskManager.Application.Tasks.Command
{
    /// <summary>
    /// Validator for <see cref="CreateTaskCommand"/>
    /// </summary>
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(p => p.Status).NotNull().WithMessage("Status is required");


            // TODO: Validate for unique title
        }
    }
}
