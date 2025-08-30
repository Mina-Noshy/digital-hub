using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Master.UnitCategoryMaster;
using DigitalHub.Application.Services.Master.UnitCategoryMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Master;

[Route(ApiVersionsConfig.Api.Routes.Master + "unit-category-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_2_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UnitCategoryMasterController : BaseApiController
{
    [HttpGet("dropdown")]
    public async Task<IActionResult> GetAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetUnitCategoryMastersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUnitCategoryMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUnitCategoryMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUnitCategoryMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUnitCategoryMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateUnitCategoryMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateUnitCategoryMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteUnitCategoryMasterCommand(id, cancellationToken));

}
