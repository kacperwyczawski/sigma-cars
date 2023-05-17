using MediatR;
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
        var carType = await _dbContext
                           .Set<Domain.Models.CarType>()
                           .FindAsync(new object?[] { query.carTypeId }, cancellationToken: cancellationToken)
                       ?? throw new NotFoundException(nameof(Domain.Models.CarType), "get");

        return new GetCarsResponse(carType.Cars.Select(
            x => new GetCarResponse(
                x.Id,
                "", // TODO
                x.RegistrationNumber)));
    }
}