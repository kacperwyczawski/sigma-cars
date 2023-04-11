using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Application.Features.CarModel.Requests;

public record CreateCarModelRequest(
    string Make,
    string Model,
    int ProductionYear,
    string Color,
    float PricePerDay,
    int SeatCount
);