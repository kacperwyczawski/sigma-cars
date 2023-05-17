using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType.Commands;

public class DeletecarTypeHandler : IRequestHandler<DeletecarTypeCommand>
{
    private readonly SigmaCarsDbContext _dbContext;

    public DeletecarTypeHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeletecarTypeCommand command, CancellationToken cancellationToken)
    {
        var carType = await _dbContext.Set<Domain.Models.CarType>()
                           .FindAsync(new object?[] { command.Id }, cancellationToken: cancellationToken)
                       ?? throw new NotFoundException(nameof(Domain.Models.CarType), "delete");

        _dbContext.Remove(carType);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}