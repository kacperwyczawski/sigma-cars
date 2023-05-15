using MediatR;
using Microsoft.AspNetCore.Mvc;
using SigmaCars.WebApi.Features.Rental.Queries;

namespace SigmaCars.WebApi.Features.User;

[ApiController]
[Route("users/{id:int}")]
public class UsersController : Controller
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("rentals")]
    public async Task<IActionResult> GetRentals(int id)
    {
        var result = await _mediator.Send(new GetRentalsQuery(id));
        
        if (!result.Rentals.Any())
            return NoContent();

        return Ok(result);
    }
    
    [HttpDelete("rentals/{rentalId:int}")]
    public async Task<IActionResult> DeleteRental(int id, int rentalId)
    {
        throw new NotImplementedException();
    }
}