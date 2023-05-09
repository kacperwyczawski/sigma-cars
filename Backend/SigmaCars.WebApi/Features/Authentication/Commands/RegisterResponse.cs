namespace SigmaCars.WebApi.Features.Authentication.Commands;

public record RegisterResponse(
    string FirstName,
    string LastName,
    string Email,
    string Jwt);