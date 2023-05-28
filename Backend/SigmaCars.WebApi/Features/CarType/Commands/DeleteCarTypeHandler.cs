using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public class DeleteCarTypeHandler : IRequestHandler<DeleteCarTypeCommand>
{
    private readonly SigmaCarsDbContext _dbContext;

    public DeleteCarTypeHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCarTypeCommand command, CancellationToken cancellationToken)
    {
        var carType = await _dbContext.Set<Domain.Models.CarType>()
                           .FindAsync(new object?[] { command.Id }, cancellationToken: cancellationToken)
                       ?? throw new NotFoundException(nameof(Domain.Models.CarType), "delete");

        _dbContext.Remove(carType);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}