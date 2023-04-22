namespace SigmaCars.Application.Features.Authentication.Commands;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email,
    string Jwt);