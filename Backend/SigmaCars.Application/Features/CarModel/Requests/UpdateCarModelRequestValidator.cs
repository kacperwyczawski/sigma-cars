using FluentValidation;

namespace SigmaCars.Application.Features.CarModel.Requests;

public class UpdateCarModelRequestValidator : AbstractValidator<UpdateCarModelRequest>
{
    public UpdateCarModelRequestValidator()
    {
        RuleFor(x => x.Id)
            .InclusiveBetween(0, int.MaxValue);
        RuleFor(x => x.Make)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.ProductionYear)
            .InclusiveBetween(0, int.MaxValue);
        RuleFor(x => x.Color)
            .NotEmpty()
            .MaximumLength(50);
        RuleFor(x => x.PricePerDay)
            .InclusiveBetween(0, float.MaxValue);
        RuleFor(x => x.SeatCount)
            .InclusiveBetween(0, int.MaxValue);
    }
}