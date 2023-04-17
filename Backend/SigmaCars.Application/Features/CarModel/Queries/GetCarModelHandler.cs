using MediatR;
using SigmaCars.Application.Features.Car.Persistence;
using SigmaCars.Application.Features.CarModel.Persistence;

namespace SigmaCars.Application.Features.CarModel.Queries;

public class GetCarModelHandler : IRequestHandler<GetCarModelsQuery, IEnumerable<GetCarModelResponse>>
{
    private readonly ICarModelsService _carModelsService;

    private readonly ICarsService _carsService;

    public GetCarModelHandler(ICarModelsService carModelsService, ICarsService carsService)
    {
        _carModelsService = carModelsService;
        _carsService = carsService;
    }

    public async Task<IEnumerable<GetCarModelResponse>> Handle(
        GetCarModelsQuery query,
        CancellationToken cancellationToken)
    {
        var carModels = await _carModelsService.GetAsync(query);

        return await Task.WhenAll(carModels.Select
        (
            async carModel =>
            {
                var availableCarIds =
                    await _carsService.GetAvailableCarsIdsAsync(carModel.Id, query.StartDate, query.EndDate);
                var allCarIds = await _carsService.GetCarIdsAsync(carModel.Id);

                return new GetCarModelResponse
                (
                    carModel.Id,
                    carModel.Make,
                    carModel.Model,
                    carModel.ProductionYear,
                    carModel.Color,
                    carModel.PricePerDay,
                    carModel.SeatCount,
                    availableCarIds.Count(),
                    availableCarIds,
                    allCarIds
                );
            }));
    }
}