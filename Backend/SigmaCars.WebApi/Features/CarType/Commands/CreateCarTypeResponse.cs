namespace SigmaCars.WebApi.Features.CarType.Commands;

public record CreateCarTypeResponse(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    float PricePerDay,
    int SeatCount)
{
    public static CreateCarTypeResponse FromCarType(Domain.Models.CarType carType) => new(
        carType.Id,
        carType.Make,
        carType.Model,
        carType.ProductionYear,
        carType.PricePerDay,
        carType.SeatCount);
}