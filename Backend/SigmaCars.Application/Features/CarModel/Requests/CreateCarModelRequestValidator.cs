using FluentValidation;

namespace SigmaCars.Application.Features.CarModel.Requests;

public class CreateCarModelRequestValidator : AbstractValidator<CreateCarModelRequest>
{
    public CreateCarModelRequestValidator()
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