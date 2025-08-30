using DigitalHub.Domain.Constants;
using System.Security.Claims;

namespace DigitalHub.Domain.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long? GetUserId(this ClaimsPrincipal? principal)
        => Convert.ToInt64(principal?.FindFirstValue(UserClaims.USER_ID));

    public static string? GetUsername(this ClaimsPrincipal? principal)
        => principal?.FindFirstValue(UserClaims.USERNAME);

    public static string? GetUserFullname(this ClaimsPrincipal? principal)
        => principal?.FindFirstValue(UserClaims.USER_FULLNAME);

    public static string? GetUserEmail(this ClaimsPrincipal? principal)
        => principal?.FindFirstValue(UserClaims.USER_EMAIL);

    public static string[] GetUserRoles(this ClaimsPrincipal? principal)
        => principal?.Claims
            .Where(c => c.Type == UserClaims.USER_ROLES)
            .Select(c => c.Value)
            .ToArray() ?? Array.Empty<string>();

}

