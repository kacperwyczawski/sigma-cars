using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaCars.WebApi.Features.Car.Commands;
using SigmaCars.WebApi.Features.Car.Queries;
using SigmaCars.WebApi.Features.CarType.Commands;
using SigmaCars.WebApi.Features.CarType.Queries;
using SigmaCars.WebApi.Features.CarType.Requests;
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
        [FromQuery(Name = "available-only")] bool availableOnly = true)
    {
        var request = new GetcarTypesQuery(
            startDate, endDate,
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make, model,
            orderByPropertyName, ascending,
            availableOnly);

        var result = await _mediator.Send(request);

        if (!result.carTypes.Any())
            return NoContent();

        return Ok(result);
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

    [HttpDelete("{id:int}/cars/{carId:int}")]
    public async Task<IActionResult> DeleteCar(int id, int carId)
    {
        await _mediator.Send(new DeleteCarCommand(carId));

        return NoContent();
    }

    [HttpPost("{id:int}/cars")]
    public async Task<IActionResult> PostCar(int id, [FromBody] CreateCarCommand request)
    {
        var created = await _mediator.Send(request);

        return Created($"car-types/{id}/cars/{created.Id}", created);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCarTypeCommand request)
    {
        var created = await _mediator.Send(request);

        return Created($"car-types/{created.Id}", created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletecarTypeCommand(id));

        return NoContent();
    }

    [HttpPost("{carTypeId:int}/rentals")]
    public async Task<IActionResult> PostRental([FromBody] PostRentalRequest request, int carTypeId)
    {
        var carId = await _dbContext.Cars
            .Where(c => c.carTypeId == carTypeId)
            .Select(c => c.Id)
            .FirstOrDefaultAsync();
        
        // TODO: 👆
        // this thing rents the same car over and over again
        // it should rent the car that is available for the given period
        // amen

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