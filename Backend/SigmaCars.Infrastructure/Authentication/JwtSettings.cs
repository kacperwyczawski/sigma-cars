namespace SigmaCars.Infrastructure.Authentication;

public class JwtSettings
{
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int MinutesToExpire { get; init; }
    public string Key { get; init; } = null!;
}