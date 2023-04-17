﻿namespace SigmaCars.Application.Features.CarModel.Commands;

public record CreateCarModelResponse(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    string Color,
    float PricePerDay,
    int SeatCount)
{
    public static CreateCarModelResponse FromCarModel(Domain.Models.CarModel carModel) => new(
        carModel.Id,
        carModel.Make,
        carModel.Model,
        carModel.ProductionYear,
        carModel.Color,
        carModel.PricePerDay,
        carModel.SeatCount);
}