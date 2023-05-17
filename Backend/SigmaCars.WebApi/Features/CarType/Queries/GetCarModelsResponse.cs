namespace SigmaCars.WebApi.Features.CarType.Queries;

public record GetcarTypesResponse(
    IEnumerable<GetcarTypeResponse> carTypes);

public record GetcarTypeResponse(
    int Id,
    string Make,
    string Model,
    float PricePerDay,
    int SeatCount);