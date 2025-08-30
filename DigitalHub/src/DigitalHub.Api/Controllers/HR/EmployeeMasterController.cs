using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Application.Services.Master.EmployeeMaster;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.QueryParams.HR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.HR;

[Route(ApiVersionsConfig.Api.Routes.Hr + "employee-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_3_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class EmployeeMasterController : BaseApiController
{
    [HttpGet("dropdown")]
    public async Task<IActionResult> GetAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetEmployeeMastersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateEmployeeMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetEmployeeMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetEmployeeMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateEmployeeMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateEmployeeMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteEmployeeMasterCommand(id, cancellationToken));
}
