using MediatR;
using Microsoft.EntityFrameworkCore;
using SigmaCars.Application.Features.CarModel.Persistence;
using SigmaCars.Domain.Exceptions;

namespace SigmaCars.Application.Features.CarModel.Commands;

public class DeleteCarModelHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly DbContext _dbContext;

    public DeleteCarModelHandler(DbContext dbContext)
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