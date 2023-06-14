using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{Name} cannot exceed 50 characters");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("{Url} is required");
        }
    }
}
