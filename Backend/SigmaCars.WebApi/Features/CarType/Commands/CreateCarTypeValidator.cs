using FluentValidation;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public class CreateCarTypeValidator : AbstractValidator<CreateCarTypeCommand>
{
    public CreateCarTypeValidator()
    {
        RuleFor(x => x.Make)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(50);
    }
}