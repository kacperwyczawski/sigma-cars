using MediatR;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Car.Commands;

public class CreateCarHandler : IRequestHandler<CreateCarCommand, CreateCarResponse>
{
    private readonly SigmaCarsDbContext _dbContext;

    public CreateCarHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateCarResponse> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        var car = new Domain.Models.Car
        {
            Id = 0,
            carTypeId = command.carTypeId,
            DepartmentId = command.DepartmentId,
            RegistrationNumber = command.RegistrationNumber,
            Vin = command.Vin
        };
          
        _dbContext.Set<Domain.Models.Car>().Add(car);
        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return CreateCarResponse.FromCar(car);
    }
}