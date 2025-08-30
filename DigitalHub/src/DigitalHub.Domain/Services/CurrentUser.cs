using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Extensions;
using DigitalHub.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Http;

namespace DigitalHub.Domain.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public long UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ?? 0; // throw new ApplicationException("User ID in context is unavailable");

    public string IpAddress =>
        httpContextAccessor
        .HttpContext?
        .Connection?
        .RemoteIpAddress?.ToString() ?? "UnKnown"; // throw new ApplicationException("User IP in context is unavailable");

    public string Username =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUsername() ?? IpAddress ?? "UnKnown"; // throw new ApplicationException("Username in context is unavailable");

    public string UserFullname =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserFullname() ?? "UnKnown"; // throw new ApplicationException("User Fullname in context is unavailable");

    public string UserEmail =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserEmail() ?? "UnKnown"; // throw new ApplicationException("User Email in context is unavailable");

    public string[] UserRoles =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserRoles() ?? throw new ApplicationException("User roles in context is unavailable");

    public string DatabaseNo =>
        httpContextAccessor.HttpContext?.Request.Headers[UserClaims.DATABASE_NO].ToString() ?? string.Empty;

    public string UserAgent =>
        httpContextAccessor
        .HttpContext?
        .Request?
        .Headers[UserClaims.USER_AGENT].ToString() ?? "UnKnown"; // throw new ApplicationException("User agent in context is unavailable");

    public string FinancialPeriod =>
        httpContextAccessor
        .HttpContext?
        .Request?
        .Headers[UserClaims.FINANCIAL_PERIOD].ToString() ?? "UnKnown"; // throw new ApplicationException("Financial period in context is unavailable");

    public string Language =>
        httpContextAccessor
        .HttpContext?
        .Request?
        .Headers[UserClaims.LANGUAGE].ToString() ?? "en";

    public string ContentDirection =>
        LtrLanguages.Contains(Language.ToLowerInvariant()) ? "ltr" : "rtl";







    private static readonly HashSet<string> LtrLanguages = new(StringComparer.OrdinalIgnoreCase)
    {
        "en", "fr", "it", "de", "es", "pt", "nl", "ru", "tr", "pl", "ro", "sv", "no", "da"
    };
}
