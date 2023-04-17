using SigmaCars.Application.Features.CarModel.Commands;
using SigmaCars.Application.Features.CarModel.Queries;

namespace SigmaCars.Application.Features.CarModel.Persistence;

public interface ICarModelsService
{
    public Task<IEnumerable<Domain.Models.CarModel>> GetAsync(GetCarModelsQuery request);

    public Task<Domain.Models.CarModel> CreateAsync(CreateCarModelCommand request);

    public Task DeleteAsync(int id);
}