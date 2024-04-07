namespace Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecretKey { get; init; } = string.Empty;
    public double AccessTokenExpirationInMinutes { get; init; }
    public double RefreshTokenExpirationInDays { get; init; }
}
