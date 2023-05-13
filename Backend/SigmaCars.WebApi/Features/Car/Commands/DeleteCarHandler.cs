using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.Car.Commands;

public class DeleteCarHandler : IRequestHandler<DeleteCarCommand>
{
    private readonly SigmaCarsDbContext _dbContext;

    public DeleteCarHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCarCommand command, CancellationToken cancellationToken)
    {
        var car = await _dbContext.Set<Domain.Models.Car>()
                      .FindAsync(new object?[] { command.Id }, cancellationToken: cancellationToken)
                  ?? throw new NotFoundException($"Car with ID {command.Id} not found.");


        _dbContext.Remove(car);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}