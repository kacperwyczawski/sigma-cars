using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType.Queries;

public class GetcarTypesHandler : IRequestHandler<GetcarTypesQuery, GetcarTypesResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetcarTypesHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetcarTypesResponse> Handle(
        GetcarTypesQuery query,
        CancellationToken cancellationToken)
    {
        var carTypes = _dbContext.Set<Domain.Models.CarType>().AsQueryable();

        if (query.MinYear != null)
            carTypes = carTypes.Where(carType => carType.ProductionYear >= query.MinYear);
        if (query.MaxYear != null)
            carTypes = carTypes.Where(carType => carType.ProductionYear <= query.MaxYear);
        if (query.MinPrice != null)
            carTypes = carTypes.Where(carType => carType.PricePerDay >= query.MinPrice);
        if (query.MaxPrice != null)
            carTypes = carTypes.Where(carType => carType.PricePerDay <= query.MaxPrice);
        if (query.MinSeats != null)
            carTypes = carTypes.Where(carType => carType.SeatCount >= query.MinSeats);
        if (query.MaxSeats != null)
            carTypes = carTypes.Where(carType => carType.SeatCount <= query.MaxSeats);
        if (query.Make != null)
            carTypes = carTypes.Where(carType => carType.Make == query.Make);
        if (query.Model != null)
            carTypes = carTypes.Where(carType => carType.Model == query.Model);

        carTypes = query.OrderByPropertyName switch
        {
            "production_year" => query.Ascending
                ? carTypes.OrderBy(carType => carType.ProductionYear)
                : carTypes.OrderByDescending(carType => carType.ProductionYear),
            "price_per_day" => query.Ascending
                ? carTypes.OrderBy(carType => carType.PricePerDay)
                : carTypes.OrderByDescending(carType => carType.PricePerDay),
            _ => carTypes
        };

        var carTypesQueried = await carTypes
            .Include(carType => carType.Cars)
            .ToListAsync(cancellationToken);

        if (query.AvailableOnly)
        {
            carTypesQueried = carTypesQueried.Where(carType =>
                carType.Cars.Any(car =>
                    car.Rentals.All(rental =>
                        rental.EndDate < query.StartDate
                        || rental.StartDate > query.EndDate))).ToList();
        }

        return new GetcarTypesResponse(
            carTypesQueried.Select(x =>
                new GetcarTypeResponse(
                    x.Id,
                    x.Make,
                    x.Model,
                    x.PricePerDay,
                    x.SeatCount)));
    }
}