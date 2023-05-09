namespace SigmaCars.WebApi.Features.Authentication.Shared;

public class JwtSettings
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int MinutesToExpiration { get; init; }
    public string Key { get; init; } = null!;
}