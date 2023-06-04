using FluentValidation;
using SigmaCars.WebApi.Features.CarType.RequestsAndResponses;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public class CreateCarTypeValidator : AbstractValidator<CreateCarTypeRequest>
{
    public CreateCarTypeValidator()
    {
        RuleFor(x => x.Make)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);
    }
}