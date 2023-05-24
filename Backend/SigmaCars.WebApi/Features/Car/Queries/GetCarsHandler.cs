using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Car.Queries;

public class GetCarsHandler : IRequestHandler<GetCarsQuery, GetCarsResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetCarsHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetCarsResponse> Handle(GetCarsQuery query, CancellationToken cancellationToken)
    {
        // check if car type exists
        var carType = await _dbContext
            .Set<Domain.Models.CarType>()
            .FindAsync(new object?[] { query.CarTypeId }, cancellationToken: cancellationToken);
        if (carType == null)
            throw new NotFoundException(nameof(Domain.Models.CarType), "get");

        var cars = await _dbContext
            .Cars
            .Where(car => car.CarTypeId == query.CarTypeId)
            .Include(car => car.Department)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new GetCarsResponse(cars.Select(
            car => new GetCarResponse(
                car.Id,
                car.Department.City,
                car.RegistrationNumber)));
    }
}