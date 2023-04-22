using MediatR;
using Microsoft.AspNetCore.Mvc;
using SigmaCars.Application.Features.Authentication.Commands;

namespace SigmaCars.WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : Controller
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand request)
    {
        var created = await _mediator.Send(request);

        return Ok(created);
    }
}