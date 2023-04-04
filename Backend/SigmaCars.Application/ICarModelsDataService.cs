using SigmaCars.Domain;

namespace SigmaCars.Application;

public interface ICarModelsDataService
{
    public Task<CarModel> GetAsync(int id);

    public Task<IEnumerable<CarModel>> GetAllAsync();

    public Task<IEnumerable<CarModel>> GetFilteredAsync(
        int minYear, int maxYear,
        float minPrice, float maxPrice,
        int minSeats, int maxSeats,
        string make = "", string model = "");

    public Task AddAsync(CarModel carModel);

    public Task UpdateAsync(CarModel carModel);

    public Task DeleteAsync(int id);
}