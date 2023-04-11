using Microsoft.AspNetCore.Mvc;
using SigmaCars.Application.Features.CarModel;
using SigmaCars.Application.Features.CarModel.Requests;
using SigmaCars.Domain.Models;

namespace SigmaCars.WebApi.Endpoints.CarModels;

[ApiController]
[Route("car-models")]
public class CarModelsController : Controller
{
    private readonly ICarModelsService _carModelsService;

    public CarModelsController(ICarModelsService carModelsService)
    {
        _carModelsService = carModelsService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _carModelsService.GetAsync(id));
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
        var request = new GetCarModelRequest(
            minYear, maxYear,
            minPrice, maxPrice,
            minSeats, maxSeats,
            make,
            model,
            orderByPropertyName,
            ascending);

        return Ok(await _carModelsService.GetAsync(request));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCarModelRequest request)
    {
        var created = await _carModelsService.CreateAsync(request);

        return Created($"car-models/{created.Id}", created);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateCarModelRequest request)
    {
        await _carModelsService.UpdateAsync(request);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carModelsService.DeleteAsync(id);
        return NoContent();
    }
}