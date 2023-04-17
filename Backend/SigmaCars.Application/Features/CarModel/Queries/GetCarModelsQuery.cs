using MediatR;

namespace SigmaCars.Application.Features.CarModel.Queries;

public record GetCarModelsQuery(
        DateTime StartDate, DateTime EndDate,
        int? MinYear, int? MaxYear,
        float? MinPrice, float? MaxPrice,
        int? MinSeats, int? MaxSeats,
        string? Make,
        string? Model,
        string? OrderByPropertyName,
        bool Ascending)
    : IRequest<IEnumerable<GetCarModelResponse>>;