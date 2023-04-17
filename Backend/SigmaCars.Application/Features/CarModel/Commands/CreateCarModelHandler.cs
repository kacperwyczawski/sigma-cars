using MediatR;
using SigmaCars.Application.Features.CarModel.Persistence;

namespace SigmaCars.Application.Features.CarModel.Commands;

public class CreateCarModelHandler : IRequestHandler<CreateCarModelCommand, CreateCarModelResponse>
{
    private readonly ICarModelsService _carModelsService;

    public CreateCarModelHandler(ICarModelsService carModelsService)
    {
        _carModelsService = carModelsService;
    }

    public async Task<CreateCarModelResponse> Handle(CreateCarModelCommand command, CancellationToken cancellationToken)
    {
        var carModel = await _carModelsService.CreateAsync(command);
        return CreateCarModelResponse.FromCarModel(carModel);
    }
}