using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Application.Services.Master.MaritalStatusMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Master;

[Route(ApiVersionsConfig.Api.Routes.Master + "marital-status-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_2_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MaritalStatusMasterController : BaseApiController
{
    [HttpGet("dropdown")]
    public async Task<IActionResult> GetAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetMaritalStatusMastersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMaritalStatusMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateMaritalStatusMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetMaritalStatusMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetMaritalStatusMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateMaritalStatusMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateMaritalStatusMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteMaritalStatusMasterCommand(id, cancellationToken));

}
