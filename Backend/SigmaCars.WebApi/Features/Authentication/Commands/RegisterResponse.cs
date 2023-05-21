namespace SigmaCars.WebApi.Features.Authentication.Commands;

public record RegisterResponse(
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string Jwt);