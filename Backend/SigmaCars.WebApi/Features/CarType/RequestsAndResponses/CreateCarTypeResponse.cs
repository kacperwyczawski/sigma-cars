namespace SigmaCars.WebApi.Features.CarType.RequestsAndResponses;

public record CreateCarTypeResponse(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    float PricePerDay,
    int SeatCount,
    string ImageUrl)
{
    public static CreateCarTypeResponse FromCarType(Domain.Models.CarType carType) => new(
        carType.Id,
        carType.Make,
        carType.Model,
        carType.ProductionYear,
        carType.PricePerDay,
        carType.SeatCount,
        $"/api/car-types/{carType.Id}/image");
}