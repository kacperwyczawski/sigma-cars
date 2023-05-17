namespace SigmaCars.WebApi.Features.CarType.Commands;

public record CreatecarTypeResponse(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    float PricePerDay,
    int SeatCount)
{
    public static CreatecarTypeResponse FromcarType(Domain.Models.CarType carType) => new(
        carType.Id,
        carType.Make,
        carType.Model,
        carType.ProductionYear,
        carType.PricePerDay,
        carType.SeatCount);
}