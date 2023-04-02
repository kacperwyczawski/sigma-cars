using SigmaCars.Domain;

namespace SigmaCars.Application;

public interface ICarModelsDataService
{
    public Task<CarModel> GetAsync(int id);
    
    public Task<IEnumerable<CarModel>> GetAllAsync();
    
    public Task<IEnumerable<CarModel>> GetByNameAsync(string name);
    
    public Task AddAsync(CarModel carModel);
    
    public Task UpdateAsync(CarModel carModel);
    
    public Task DeleteAsync(int id);
}