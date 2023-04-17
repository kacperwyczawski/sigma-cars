using MediatR;
using SigmaCars.Application.Features.CarModel.Persistence;

namespace SigmaCars.Application.Features.CarModel.Commands;

public class DeleteCarModelHandler : IRequestHandler<DeleteCarModelCommand>
{
    private readonly ICarModelsService _carModelsService;

    public DeleteCarModelHandler(ICarModelsService carModelsService)
    {
        _carModelsService = carModelsService;
    }

    public async Task Handle(DeleteCarModelCommand command, CancellationToken cancellationToken)
    {
        await _carModelsService.DeleteAsync(command.Id);
    }
}