namespace SigmaCars.WebApi.Features.CarType.Queries;

public record GetCarTypesResponse(
    IEnumerable<GetCarTypeResponse> CarTypes);

public record GetCarTypeResponse(
    int Id,
    string Make,
    string Model,
    float PricePerDay,
    int SeatCount);