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
        var carModel = await _dbContext
                           .Set<Domain.Models.CarModel>()
                           .FindAsync(new object?[] { query.CarModelId }, cancellationToken: cancellationToken)
                       ?? throw new NotFoundException(nameof(Domain.Models.CarModel), "get");

        return new GetCarsResponse(carModel.Cars.Select(
            x => new GetCarResponse(
                x.Id,
                "", // TODO
                x.RegistrationNumber)));
    }
}