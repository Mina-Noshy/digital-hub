using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Application.Services.Auth.RolePageMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "role-page-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RolePageMasterController : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRolePageMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateRolePageMasterCommand(request, cancellationToken));

    [HttpPost("add-range")]
    public async Task<IActionResult> CreateRange([FromBody] IEnumerable<CreateRolePageMasterDto> request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateRangeRolePageMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetRolePageMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetRolePageMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateRolePageMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateRolePageMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteRolePageMasterCommand(id, cancellationToken));

}
