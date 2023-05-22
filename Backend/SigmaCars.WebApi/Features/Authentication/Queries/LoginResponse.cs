namespace SigmaCars.WebApi.Features.Authentication.Queries;

public record LoginResponse(
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Jwt);