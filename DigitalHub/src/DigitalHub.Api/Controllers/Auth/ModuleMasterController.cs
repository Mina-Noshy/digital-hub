using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Auth.ModuleMaster;
using DigitalHub.Application.Services.Auth.ModuleMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "module-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ModuleMasterController : BaseApiController
{
    [HttpGet("dropdown")]
    public async Task<IActionResult> GetAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetModuleMastersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModuleMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateModuleMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetModuleMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetModuleMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateModuleMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateModuleMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteModuleMasterCommand(id, cancellationToken));

}
