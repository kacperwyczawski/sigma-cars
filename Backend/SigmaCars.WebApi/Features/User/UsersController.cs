using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SigmaCars.WebApi.Features.User;

[ApiController]
[Route("users/{id:int}")]
public class UsersController
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
        throw new NotImplementedException();
    }
    
    [HttpDelete("rentals/{rentalId:int}")]
    public async Task<IActionResult> DeleteRental(int id, int rentalId)
    {
        throw new NotImplementedException();
    }
}