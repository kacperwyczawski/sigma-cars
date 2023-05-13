using MediatR;
using SigmaCars.Domain.Exceptions;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarModel.Commands;

public class DeleteCarModelHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly SigmaCarsDbContext _dbContext;

    public DeleteCarModelHandler(SigmaCarsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCarModelCommand command, CancellationToken cancellationToken)
    {
        var carModel = await _dbContext.Set<Domain.Models.CarModel>()
                           .FindAsync(new object?[] { command.Id }, cancellationToken: cancellationToken)
                       ?? throw new NotFoundException(nameof(Domain.Models.CarModel), "delete");

        _dbContext.Remove(carModel);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}