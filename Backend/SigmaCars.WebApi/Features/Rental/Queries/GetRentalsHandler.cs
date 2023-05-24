using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Rental.Queries;

public class GetRentalsHandler : IRequestHandler<GetRentalsQuery, GetRentalsResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public GetRentalsHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<GetRentalsResponse> Handle(GetRentalsQuery query, CancellationToken cancellationToken)
    {
        var result = _dbContext.Rentals
            .Where(rental => rental.User.Id == query.UserId)
            .Include(rental => rental.Car)
            .ThenInclude(car => car.Department)
            .Select(x => new GetRentalResponse(
                x.Id,
                x.Car.CarType.Make,
                x.Car.CarType.Model,
                x.Car.RegistrationNumber,
                x.Car.Department.City,
                x.StartDate,
                x.EndDate));

        return Task.FromResult(new GetRentalsResponse(result));
    }
}