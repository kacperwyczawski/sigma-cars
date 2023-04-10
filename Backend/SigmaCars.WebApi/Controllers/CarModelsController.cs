using Microsoft.AspNetCore.Mvc;
using SigmaCars.Application.Features.CarModel;
using SigmaCars.Application.Features.CarModel.Requests;
using SigmaCars.Domain.Models;

namespace SigmaCars.WebApi.Controllers;

[ApiController]
[Route("car-models")]
public class CarModelsController : Controller
{
    private readonly ICarModelsDataService _carModelsDataService;

    public CarModelsController(ICarModelsDataService carModelsDataService)
    {
        _carModelsDataService = carModelsDataService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _carModelsDataService.GetAsync(id));
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery(Name = "min-year")] int? minYear,
        [FromQuery(Name = "max-year")] int? maxYear,
        [FromQuery(Name = "min-price")] float? minPrice,
        [FromQuery(Name = "max-price")] float? maxPrice,
        [FromQuery(Name = "min-seats")] int? minSeats,
        [FromQuery(Name = "max-seats")] int? maxSeats,
        [FromQuery(Name = "make")] string? make,
        [FromQuery(Name = "model")] string? model,
        [FromQuery(Name = "order-by")] string? orderByPropertyName,
        [FromQuery(Name = "ascending")] bool ascending = true)
    {
        return Ok(await _carModelsDataService.GetAsync(
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make,
            model,
            orderByPropertyName,
            ascending));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddCarModelRequest request)
    {
        var created = await _carModelsDataService.AddAsync(request);

        return Created($"car-models/{created.Id}", created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] CarModel carModel)
    {
        await _carModelsDataService.UpdateAsync(carModel);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carModelsDataService.DeleteAsync(id);
        return NoContent();
    }
}