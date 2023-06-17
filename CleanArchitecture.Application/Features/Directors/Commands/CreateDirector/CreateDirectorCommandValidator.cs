using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{Name} is required")
                .NotEmpty().WithMessage("{Name} is required");

            RuleFor(x => x.LastName)
                .NotNull().WithMessage("{LastName} is required")
                .NotEmpty().WithMessage("{LastName} is required");
        }
    }
}
