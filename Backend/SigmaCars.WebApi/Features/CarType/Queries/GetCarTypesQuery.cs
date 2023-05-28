using MediatR;

namespace SigmaCars.WebApi.Features.CarType.Queries;

public record GetCarTypesQuery(
        DateTime StartDate, DateTime EndDate,
        int? MinYear, int? MaxYear,
        float? MinPrice, float? MaxPrice,
        int? MinSeats, int? MaxSeats,
        string? Make,
        string? Model,
        string? OrderByPropertyName,
        bool Ascending, bool ShowAll,
        int DepartmentId)
    : IRequest<GetCarTypesResponse>;