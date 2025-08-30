using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Application.Services.Auth.Auth;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "auth")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
public class AuthController(IEmailSender emailSender, ICompanyProfileSettingsRepository settingsRepository) : BaseApiController
{

    //============================================> Testing Area
    [HttpPost("test-ratelimiter")]
    public async Task<IActionResult> RateLimiterTesting(CancellationToken cancellationToken)
            => await Task.FromResult(Ok("success"));



    [HttpPost("test-email")]
    public async Task<IActionResult> EmailSender(string email, CancellationToken cancellationToken)
    {
        var settings = await settingsRepository.GetSettingsAsync(cancellationToken);

        var emailBody = settings!.HTMLEmailHeader + "<h1>Test Email</h1>" +
                        settings.HTMLEmailFooter;

        var isSent = await emailSender
            .Compose()
            .To(email)
            .Subject("Test Email")
            .Body(emailBody, true)
            .SendAsync(cancellationToken);

        return Ok(isSent);
    }

    //[HttpPost("test-password")]
    //public async Task<IActionResult> TestPassword(CancellationToken cancellationToken)
    //        => await Task.FromResult(Ok(PasswordGeneratorHelper.GeneratePassword(8, 4, 0, 4, 0)));

    //============================================> Testing Area







    [HttpPost("get-token")]
    public async Task<IActionResult> GetToken(GetTokenDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new GetTokenQuery(request, cancellationToken));

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new RefreshTokenCommand(request, cancellationToken));

    [HttpPost("revoke-token")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RevokeToken(TokenDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new RevokeTokenCommand(request, cancellationToken));

    [HttpPost("add-user-role")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddUserRole(UserToRoleDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new AddUserRoleCommand(request, cancellationToken));

    [HttpDelete("remove-user-role")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RemoveUserRole(UserToRoleDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new RemoveUserRoleCommand(request, cancellationToken));

    [HttpGet("user-modules")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UserModules(CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserModulesQuery(cancellationToken));

    [HttpGet("user-module-pages/{moduleId}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UserModulePages(long moduleId, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserModulePagesQuery(moduleId, cancellationToken));

    [HttpPut("change-password")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new ChangePasswordCommand(request, cancellationToken));

    [HttpPut("add-password-internal")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddPassword(CancellationToken cancellationToken)
            => await Mediator.Send(new AddPasswordInternalCommand(cancellationToken));

    [HttpPut("add-password-external")]
    public async Task<IActionResult> AddPasswordByEmail(string email, CancellationToken cancellationToken)
            => await Mediator.Send(new AddPasswordExternalCommand(email, cancellationToken));
}
