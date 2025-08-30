using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.Services.Auth.UserLoginHistoryMaster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "user-login-history-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserLoginHistoryMasterController : BaseApiController
{

    [HttpGet("by-user/{userId:long}")]
    public async Task<IActionResult> GetById(long userId, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserLoginHistoryMastersByUserIdQuery(userId, cancellationToken));

}
