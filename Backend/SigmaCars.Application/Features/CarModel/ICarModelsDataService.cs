using SigmaCars.Application.Features.CarModel.Requests;

namespace SigmaCars.Application.Features.CarModel;

public interface ICarModelsDataService
{
    public Task<Domain.Models.CarModel> GetAsync(int id);

    public Task<IEnumerable<Domain.Models.CarModel>> GetAsync(GetCarModelRequest request);

    public Task<Domain.Models.CarModel> CreateAsync(CreateCarModelRequest request);

    public Task UpdateAsync(UpdateCarModelRequest request);

    public Task DeleteAsync(int id);
}