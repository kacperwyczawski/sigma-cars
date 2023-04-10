using System.ComponentModel.DataAnnotations;

namespace SigmaCars.Application.Features.CarModel.Requests;

public record UpdateCarModelRequest(
    int Id,
    [MaxLength(50)] string Make,
    [MaxLength(50)] string Model,
    int ProductionYear,
    [MaxLength(50)] string Color,
    [Range(0, float.MaxValue)] float PricePerDay,
    [Range(0, int.MaxValue)] int SeatCount
);