using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Domain.Constants;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;

using CompanyProfileSettingsEntity = DigitalHub.Domain.Entities.Auth.CompanyProfileSettings;

namespace DigitalHub.Application.Services.Auth.Common;

internal static class AuthHelper
{
    internal static string? GetConfirmationMessage(UserMaster user, string userNameFromRequest)
    {
        if (!user.EmailConfirmed && !user.PhoneNumberConfirmed)
        {
            return ApiMessage.ConfirmationRequired;
        }
        else if (!user.EmailConfirmed && user.Email == userNameFromRequest)
        {
            return ApiMessage.ConfirmationRequired;
        }
        else if (!user.PhoneNumberConfirmed && user.PhoneNumber == userNameFromRequest)
        {
            return ApiMessage.ConfirmationRequired;
        }

        return null;
    }

    internal static UserInfoDto ConfigureAndGetUserInfo(UserMaster user, DateTime currentDateTime, IPathFinderService _pathFinderService, List<string>? roles)
    {
        var userInfoDto = new UserInfoDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            FullName = user.FullName,
            UserName = user?.UserName ?? throw new ArgumentNullException(nameof(user.UserName)),
            Email = user?.Email ?? throw new ArgumentNullException(nameof(user.UserName)),
            AccessToken = GenerateAccessToken(user, roles, currentDateTime),
            RefreshToken = GenerateRefreshToken(),
            TokenExpiration = currentDateTime.AddMinutes(int.Parse(ConfigurationHelper.GetJWT(JWTKeys.ExpiryMinutes))),
            Roles = roles,
        };

        if (!string.IsNullOrWhiteSpace(user.ProfileImage))
        {
            var folderPath = _pathFinderService.UserProfileFolderPath;
            userInfoDto.ProfileImageUrl = _pathFinderService.GetFullURL(folderPath, user.ProfileImage);
        }

        return userInfoDto;
    }

    internal async static Task<bool> SendConfirmationEmailAsync(UserMaster user, CompanyProfileSettingsEntity companyProfile, HttpRequest request, IEmailSender _emailSender, string token, CancellationToken cancellationToken = default)
    {
        var confirmationUrl = BuildConfirmationCallbackUrl(request, user.Id, token);

        var companyName = companyProfile!.CompanyName;

        var subject =
            ConfigurationHelper.GetLocalizedEmailString(EmailTemplateKeys.EmailConfirmationSubject)
            .Replace("{{company}}", companyName);

        string emailBody = ConfigurationHelper.GetLocalizedEmailString(EmailTemplateKeys.EmailConfirmationBody);

        string body = emailBody
            .Replace("{{company}}", companyName)
            .Replace("{{confirmation-url}}", confirmationUrl);

        var isSent = await _emailSender
            .Compose()
            .To(user.Email!)
            .Subject(subject)
            .Body(body, true)
            .SendAsync(cancellationToken);

        return isSent;
    }












    private static string BuildConfirmationCallbackUrl(HttpRequest request, long userId, string token)
    {
        if (request == null) throw new InvalidOperationException("Request context is unavailable.");

        // Get the base URL dynamically
        var apiUrl = $"{request.Scheme}://{request.Host.Value}".TrimEnd('/');
        const string endpoint = "/api/v1/auth/accounts/confirm-email";

        return $"{apiUrl}{endpoint}?userId={userId}&token={UrlEncoder.Default.Encode(token)}";
    }

    private static string GenerateAccessToken(UserMaster user, List<string>? roles, DateTime currentDateTime)
    {
        var roleClaims = new List<Claim>();

        if (roles != null)
        {
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? throw new ArgumentNullException(nameof(user.UserName))),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? throw new ArgumentNullException(nameof(user.Email))),
            new Claim(UserClaims.USER_ID, user.Id.ToString()),
            new Claim(UserClaims.USERNAME, user.UserName),
            new Claim(UserClaims.USER_FULLNAME, user.FullName),
            new Claim(UserClaims.USER_EMAIL, user.Email),
        }
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.GetJWT(JWTKeys.Secret)));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: ConfigurationHelper.GetJWT(JWTKeys.Issuer),
            audience: ConfigurationHelper.GetJWT(JWTKeys.Audience),
            claims: claims,
            expires: currentDateTime.AddMinutes(int.Parse(ConfigurationHelper.GetJWT(JWTKeys.ExpiryMinutes))),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }


}
