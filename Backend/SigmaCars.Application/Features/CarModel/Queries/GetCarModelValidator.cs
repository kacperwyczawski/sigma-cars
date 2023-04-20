﻿using FluentValidation;

namespace SigmaCars.Application.Features.CarModel.Queries;

public class GetCarModelValidator : AbstractValidator<GetCarModelsQuery>
{
    public GetCarModelValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .LessThan(x => x.EndDate);

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .LessThan(x => x.StartDate.AddMonths(4));
        
        RuleFor(x => x.MaxYear)
            .GreaterThan(x => x.MinYear)
            .When(x => x.MinYear.HasValue);

        RuleFor(x => x.MaxPrice)
            .GreaterThan(x => x.MinPrice)
            .When(x => x.MinPrice.HasValue);

        RuleFor(x => x.MaxSeats)
            .GreaterThan(x => x.MinSeats)
            .When(x => x.MinSeats.HasValue);

        RuleFor(x => x.Make)
            .MaximumLength(50);
        RuleFor(x => x.Model)
            .MaximumLength(50);

        RuleFor(x => x.OrderByPropertyName)
            .Must(x => x is "production_year" or "price_per_day" or null);
    }
}