using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType.Queries;

public class GetCarTypesHandler : IRequestHandler<GetCarTypesQuery, GetCarTypesResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetCarTypesHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCarTypesResponse> Handle(
        GetCarTypesQuery query,
        CancellationToken cancellationToken)
    {
        var carTypes = _dbContext.Set<Domain.Models.CarType>().AsQueryable();

        // basic filters
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

        // order by
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

        // query database
        var carTypesQueried = await carTypes
            .Include(carType => carType.Cars)
            .ThenInclude(car => car.Rentals)
            .ToListAsync(cancellationToken);

        // filter by availability
        if (!query.ShowAll)
        {
            carTypesQueried = carTypesQueried
                .Where(carType => carType.Cars
                    .Where(car => car.DepartmentId == query.DepartmentId)
                    .Any(car => car.Rentals
                        .All(rental =>
                            rental.EndDate < query.StartDate
                            || rental.StartDate > query.EndDate))).ToList();
        }

        return new GetCarTypesResponse(
            carTypesQueried.Select(x =>
                new GetCarTypeResponse(
                    x.Id,
                    x.Make,
                    x.Model,
                    x.PricePerDay,
                    x.SeatCount)));
    }
}