using FluentValidation;

namespace SigmaCars.WebApi.Features.CarModel.Commands;

public class CreateCarModelValidator : AbstractValidator<CreateCarModelCommand>
{
    public CreateCarModelValidator()
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