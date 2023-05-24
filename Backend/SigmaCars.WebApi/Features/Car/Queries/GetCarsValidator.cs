using FluentValidation;

namespace SigmaCars.WebApi.Features.Car.Queries;

public class GetCarsValidator : AbstractValidator<GetCarsQuery>
{
    public GetCarsValidator()
    {
        RuleFor(x => x.CarTypeId)
            .NotEmpty();
    }
}