using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.Application.Persistence;

namespace SigmaCars.Application.Features.CarModel.Queries;

public class GetCarModelHandler : IRequestHandler<GetCarModelsQuery, IEnumerable<GetCarModelResponse>>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetCarModelHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GetCarModelResponse>> Handle(
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

        // var result = Enumerable.Empty<GetCarModelResponse>();
        //
        // foreach (var carModel in carModels)
        // {
        //     var availableCarIds = await _dbContext.Set<Car>()
        //         .Where(car => car.CarModelId == carModel.Id)
        //         .Where(car => !_dbContext
        //             .Set<Rental>()
        //             .Where(rental => rental.CarId == car.Id)
        //             .Where(rental => rental.StartDate <= query.EndDate)
        //             .Any(rental => rental.EndDate >= query.StartDate))
        //         .ToListAsync(cancellationToken: cancellationToken);
        //     
        //     var allCarIds = await _dbContext.Set<Car>()
        //         .Where(car => car.CarModelId == carModel.Id)
        //         .ToListAsync(cancellationToken: cancellationToken);
        //
        //     result = result.Append(new GetCarModelResponse(
        //         carModel.Id,
        //         carModel.Make,
        //         carModel.Model,
        //         carModel.ProductionYear,
        //         carModel.Color,
        //         carModel.PricePerDay,
        //         carModel.SeatCount,
        //         availableCarIds.Count,
        //         availableCarIds.Select(car => car.Id),
        //         allCarIds.Select(car => car.Id)));
        // }

        carModels = carModels
            .Include(carModel => carModel.Cars
                .Where(car => car.Rentals
                    .Where(rental => rental.StartDate <= query.EndDate)
                    .Any(rental => rental.EndDate >= query.StartDate)));

        var results = carModels.Select(x => new GetCarModelResponse(
            x.Id,
            x.Make,
            x.Model,
            x.ProductionYear,
            x.Color,
            x.PricePerDay,
            x.SeatCount,
            x.Cars.Count,
            x.Cars.Select(car => car.Id),
            x.Cars.Select(car => car.Id)));

        return results;
    }
}