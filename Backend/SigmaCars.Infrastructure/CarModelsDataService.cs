using System.Data;
using Dapper;
using SigmaCars.Application;
using SigmaCars.Domain;

namespace SigmaCars.Infrastructure;

public class CarModelsDataService : ICarModelsDataService
{
    private readonly IDbConnection _connection;

    public CarModelsDataService(IDbConnection connection)
    {
        _connection = connection;
    }

    public Task<CarModel> GetAsync(int id)
    {
        return _connection.QueryFirstAsync<CarModel>("select * from car_models where id = @Id", new { Id = id });
    }

    public Task<IEnumerable<CarModel>> GetAllAsync()
    {
        return _connection.QueryAsync<CarModel>("select * from car_models");
    }

    public Task<IEnumerable<CarModel>> GetFilteredAsync(
            int minYear, int maxYear,
            float minPrice, float maxPrice,
            int minSeats, int maxSeats,
            string make = "", string model = "")
    {
        return _connection.QueryAsync<CarModel>(
            "select * from car_models " +
            "where make like @Make and model like @Model " +
            "and production_year between @MinYear and @MaxYear " +
            "and price between @MinPrice and @MaxPrice " +
            "and seatCount between @MinSeats and @MaxSeats",
            new
            {
                Make = $"%{make}%",
                Model = $"%{model}%",
                MinYear = minYear,
                MaxYear = maxYear,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinSeats = minSeats,
                MaxSeats = maxSeats
            });
    }

public Task AddAsync(CarModel carModel)
{
    return _connection.ExecuteAsync(
        "insert into car_models (make, model, production_year, color, price, seatCount) " +
        "values (@Make, @Model, @Year, @Color, @PricePerDay, @SeatCount)",
        carModel);
}

public Task UpdateAsync(CarModel carModel)
{
    return _connection.ExecuteAsync(
        "update car_models set make = @Make, model = @Model, production_year = @Year, color = @Color, price = @PricePerDay " +
        "where id = @Id",
        carModel);
}

public Task DeleteAsync(int id)
{
    return _connection.ExecuteAsync("delete from car_models where id = @Id", new { Id = id });
}

}