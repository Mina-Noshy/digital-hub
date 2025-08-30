using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Auth.CompanyProfileSettings;
using DigitalHub.Application.Services.Auth.CompanyProfileSettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;


[Route(ApiVersionsConfig.Api.Routes.Auth + "company-profile-settings")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CompanyProfileSettingsController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetCompanyProfileSettingsQuery(cancellationToken));

    [HttpPut]
    public async Task<IActionResult> Update(long id, [FromForm] UpdateCompanyProfileSettingsDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateCompanyProfileSettingsCommand(request, cancellationToken));

}