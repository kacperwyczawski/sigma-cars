using MediatR;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public class CreateCarTypeHandler : IRequestHandler<CreateCarTypeCommand, CreateCarTypeResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public CreateCarTypeHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateCarTypeResponse> Handle(CreateCarTypeCommand command, CancellationToken cancellationToken)
    {
        var carType = new Domain.Models.CarType
        {
            Id = 0,
            Make = command.Make,
            Model = command.Model,
            ProductionYear = command.ProductionYear,
            PricePerDay = command.PricePerDay,
            SeatCount = command.SeatCount
        };
          
        _dbContext.Set<Domain.Models.CarType>().Add(carType);
        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return CreateCarTypeResponse.FromCarType(carType);
    }
}