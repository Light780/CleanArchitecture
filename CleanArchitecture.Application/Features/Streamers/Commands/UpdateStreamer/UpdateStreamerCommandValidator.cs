using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator() 
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
