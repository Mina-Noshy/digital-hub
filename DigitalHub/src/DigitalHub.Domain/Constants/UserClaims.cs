using System.Security.Claims;

namespace DigitalHub.Domain.Constants;

public static class UserClaims
{
    // This sould be 'ClaimTypes.Role' because it used when JWT generated. 
    public const string USER_ROLES = ClaimTypes.Role;
    public const string USER_AGENT = "User-Agent";

    public const string USER_IP = "X-User-Ip";
    public const string USER_ID = "X-User-Id";
    public const string USER_EMAIL = "X-User-Email";
    public const string USERNAME = "X-Username";
    public const string USER_FULLNAME = "X-User-Fullname";
    public const string DATABASE_NO = "X-Database";
    public const string FINANCIAL_PERIOD = "X-Financial-Period";
    public const string LANGUAGE = "X-Language";
}
