using SigmaCars.Application.Features.CarModel.Requests;

namespace SigmaCars.Application.Features.CarModel;

public interface ICarModelsDataService
{
    public Task<Domain.Models.CarModel> GetAsync(int id);

    public Task<IEnumerable<Domain.Models.CarModel>> GetAsync(
        int? minYear = null, int? maxYear = null,
        float? minPrice = null, float? maxPrice = null,
        int? minSeats = null, int? maxSeats = null,
        string? make = null,
        string? model = null,
        string? orderByPropertyName = null,
        bool ascending = true);

    public Task<Domain.Models.CarModel> AddAsync(AddCarModelRequest request);

    public Task UpdateAsync(Domain.Models.CarModel carModel);

    public Task DeleteAsync(int id);
}