using MediatR;
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
            .Where(r => r.User.Id == query.UserId)
            .Select(r => new GetRentalResponse(
                r.Car.CarModel.Make,
                r.Car.CarModel.Model,
                r.Car.RegistrationNumber,
                r.StartDate,
                r.EndDate));
        
        return Task.FromResult(new GetRentalsResponse(result));
    }
}