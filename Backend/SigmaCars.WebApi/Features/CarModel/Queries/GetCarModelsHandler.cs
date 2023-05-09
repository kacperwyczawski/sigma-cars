using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarModel.Queries;

public class GetCarModelsHandler : IRequestHandler<GetCarModelsQuery, GetCarModelsResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetCarModelsHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCarModelsResponse> Handle(
        GetCarModelsQuery query,
        CancellationToken cancellationToken)
    {
        var carModels = _dbContext.Set<Domain.Models.CarModel>().AsQueryable();

        if (query.MinYear != null)
            carModels = carModels.Where(carModel => carModel.ProductionYear >= query.MinYear);
        if (query.MaxYear != null)
            carModels = carModels.Where(carModel => carModel.ProductionYear <= query.MaxYear);
        if (query.MinPrice != null)
            carModels = carModels.Where(carModel => carModel.PricePerDay >= query.MinPrice);
        if (query.MaxPrice != null)
            carModels = carModels.Where(carModel => carModel.PricePerDay <= query.MaxPrice);
        if (query.MinSeats != null)
            carModels = carModels.Where(carModel => carModel.SeatCount >= query.MinSeats);
        if (query.MaxSeats != null)
            carModels = carModels.Where(carModel => carModel.SeatCount <= query.MaxSeats);
        if (query.Make != null)
            carModels = carModels.Where(carModel => carModel.Make == query.Make);
        if (query.Model != null)
            carModels = carModels.Where(carModel => carModel.Model == query.Model);

        carModels = query.OrderByPropertyName switch
        {
            "production_year" => query.Ascending
                ? carModels.OrderBy(carModel => carModel.ProductionYear)
                : carModels.OrderByDescending(carModel => carModel.ProductionYear),
            "price_per_day" => query.Ascending
                ? carModels.OrderBy(carModel => carModel.PricePerDay)
                : carModels.OrderByDescending(carModel => carModel.PricePerDay),
            _ => carModels
        };

        var carModelsQueried = await carModels
            .Include(carModel => carModel.Cars)
            .ToListAsync(cancellationToken);

        if (query.AvailableOnly)
        {
            carModelsQueried = carModelsQueried.Where(carModel =>
                carModel.Cars.Any(car =>
                    car.Rentals.All(rental =>
                        rental.EndDate < query.StartDate
                        || rental.StartDate > query.EndDate))).ToList();
        }

        return new GetCarModelsResponse(
            carModelsQueried.Select(x =>
                new GetCarModelResponse(
                    x.Id,
                    x.Make,
                    x.Model,
                    x.PricePerDay,
                    x.SeatCount)));
    }
}