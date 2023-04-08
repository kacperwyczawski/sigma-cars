using SigmaCars.Application.Features.CarModel.Requests;

namespace SigmaCars.Application.Features.CarModel;

public interface ICarModelsDataService
{
    public Task<Domain.Models.CarModel> GetAsync(int id);

    public Task<IEnumerable<Domain.Models.CarModel>> GetAllAsync();

    public Task<IEnumerable<Domain.Models.CarModel>> GetFilteredAsync(GetFilteredCarModelsRequest request);

    public Task<Domain.Models.CarModel> AddAsync(AddCarModelRequest request);

    public Task UpdateAsync(Domain.Models.CarModel carModel);

    public Task DeleteAsync(int id);
}