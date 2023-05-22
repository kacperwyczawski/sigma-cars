namespace SigmaCars.WebApi.Features.Departments;

public record PostRequest(
    string City,
    string CountryCode, // ISO 3166-1 alpha-2 country code (two letters)
    string Address);