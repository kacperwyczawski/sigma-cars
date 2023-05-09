namespace SigmaCars.WebApi.Features.CarModel.Queries;

public record GetCarModelsResponse(
    IEnumerable<GetCarModelResponse> CarModels);

public record GetCarModelResponse(
    int Id,
    string Make,
    string Model,
    float PricePerDay,
    int SeatCount);