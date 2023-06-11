using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Features.Car.Commands;
using SigmaCars.WebApi.Features.Car.Queries;
using SigmaCars.WebApi.Features.CarType.Commands;
using SigmaCars.WebApi.Features.CarType.Queries;
using SigmaCars.WebApi.Features.CarType.RequestsAndResponses;
using SigmaCars.WebApi.Persistence;

namespace SigmaCars.WebApi.Features.CarType;

[ApiController]
[Route("car-types")]
public class CarTypesController : Controller
{
    private readonly IMediator _mediator;

    private readonly SigmaCarsDbContext _dbContext;

    public CarTypesController(IMediator mediator, SigmaCarsDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "start-date")] DateTime startDate,
        [FromQuery(Name = "end-date")] DateTime endDate,
        [FromQuery(Name = "min-year")] int? minYear,
        [FromQuery(Name = "max-year")] int? maxYear,
        [FromQuery(Name = "min-price")] float? minPrice,
        [FromQuery(Name = "max-price")] float? maxPrice,
        [FromQuery(Name = "min-seats")] int? minSeats,
        [FromQuery(Name = "max-seats")] int? maxSeats,
        [FromQuery(Name = "make")] string? make,
        [FromQuery(Name = "model")] string? model,
        [FromQuery(Name = "order-by")] string? orderByPropertyName,
        [FromQuery(Name = "ascending")] bool ascending = true,
        [FromQuery(Name = "show-all")] bool showAll = false,
        [FromQuery(Name = "department")] int departmentId = 0)
    {
        var request = new GetCarTypesQuery(
            startDate, endDate,
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make, model,
            orderByPropertyName, ascending,
            showAll,
            departmentId);

        var result = await _mediator.Send(request);

        if (!result.CarTypes.Any())
            return NoContent();

        return Ok(result);
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Post([FromForm] CreateCarTypeRequest request)
    {
        await using var memoryStream = new MemoryStream();
        await request.Image.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();
        
        var carType = new Domain.Models.CarType
        {
            Id = 0,
            Make = request.Make,
            Model = request.Model,
            ProductionYear = request.ProductionYear,
            PricePerDay = request.PricePerDay,
            SeatCount = request.SeatCount,
            Image = imageBytes
        };
          
        _dbContext.Set<Domain.Models.CarType>().Add(carType);
        await _dbContext.SaveChangesAsync();
    
        return Created($"car-types/{carType.Id}", CreateCarTypeResponse.FromCarType(carType));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteCarTypeCommand(id));

        return NoContent();
    }

    [HttpGet("{id:int}/image")]
    public async Task<IActionResult> GetImage(int id)
    {
        var carType = await _dbContext.CarTypes.FindAsync(id);
        if (carType == null)
            return NotFound();
        return Ok(carType.Image);
    }

    [HttpGet("{id:int}/cars")]
    public async Task<IActionResult> GetCars(int id)
    {
        var request = new GetCarsQuery(id);

        var result = await _mediator.Send(request);

        if (!result.Cars.Any())
            return NoContent();

        return Ok(result);
    }

    [HttpPost("{id:int}/cars")]
    public async Task<IActionResult> PostCar(int id, [FromBody] PostCarRequest request)
    {
        var newCar = new Domain.Models.Car
        {
            Id = 0,
            CarTypeId = id,
            DepartmentId = request.DepartmentId,
            RegistrationNumber = request.RegistrationNumber,
            Vin = new string('0', 17) // TODO: remove vin
        };

        _dbContext.Cars.Add(newCar);
        await _dbContext.SaveChangesAsync();

        var departmentCity = await _dbContext
            .Departments
            .Where(x => x.Id == request.DepartmentId)
            .Select(x => x.City)
            .FirstAsync();
        
        var response = new
        {
            newCar.Id,
            DepartmentCity = departmentCity,
            newCar.RegistrationNumber
        };
        return Created($"car-types/{id}/cars/{newCar.Id}", response);
    }

    [HttpDelete("{id:int}/cars/{carId:int}")]
    public async Task<IActionResult> DeleteCar(int id, int carId)
    {
        await _mediator.Send(new DeleteCarCommand(carId));

        return NoContent();
    }

    [HttpPost("{id:int}/rentals")]
    public async Task<IActionResult> PostRental([FromBody] PostRentalRequest request, int id)
    {
        var carId = await _dbContext.Cars
            .Where(car =>
                car.CarTypeId == id)
            .Where(car =>
                car.Rentals.All(rental =>
                    rental.EndDate < request.StartDate
                    || rental.StartDate > request.EndDate))
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        if (carId == default)
            return Problem(
                detail: "Car associated with the car model was not found.",
                statusCode: StatusCodes.Status404NotFound,
                title: "Not Found");

        var rental = new Domain.Models.Rental(
            carId,
            request.UserId,
            request.StartDate,
            request.EndDate);

        _dbContext.Rentals.Add(rental);
        await _dbContext.SaveChangesAsync();

        return Created($"users/{request.UserId}/rentals/{rental.Id}", rental);
    }
}