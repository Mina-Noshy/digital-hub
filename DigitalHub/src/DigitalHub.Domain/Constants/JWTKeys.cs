namespace DigitalHub.Domain.Constants;

public static class JWTKeys
{
    public const string Secret = "Secret";
    public const string ExpiryMinutes = "ExpiryMinutes";
    public const string Issuer = "Issuer";
    public const string Audience = "Audience";
    public const string RefreshTokenExpirationInDays = "RefreshTokenExpirationInDays";
}
