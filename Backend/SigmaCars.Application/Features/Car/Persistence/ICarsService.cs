namespace SigmaCars.Application.Features.Car.Persistence;

public interface ICarsService
{
    Task<IEnumerable<int>> GetAvailableCarsIdsAsync(int carModelId, DateTime startDate, DateTime endDate);
    
    Task<IEnumerable<int>> GetCarIdsAsync(int carModelId);
}