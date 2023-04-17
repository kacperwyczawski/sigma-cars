namespace SigmaCars.Application.Features.CarModel.Queries;

public record GetCarModelResponse(
    int Id,
    string Make,
    string Model,
    int ProductionYear,
    string Color,
    float PricePerDay,
    int SeatCount,
    int AvailableCarsCount,
    IEnumerable<int> AvailableCarIds,
    IEnumerable<int> CarIds);