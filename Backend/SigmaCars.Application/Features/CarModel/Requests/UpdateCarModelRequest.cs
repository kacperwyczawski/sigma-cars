using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Application.Features.CarModel.Requests;

public record UpdateCarModelRequest(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    string Color,
    float PricePerDay,
    int SeatCount
);