using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Master;

[Route(ApiVersionsConfig.Api.Routes.Master + "terms-and-conditions-category-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_2_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TermsAndConditionsCategoryMasterController : BaseApiController
{
    [HttpGet("dropdown")]
    public async Task<IActionResult> GetAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetTermsAndConditionsCategoryMastersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTermsAndConditionsCategoryMasterDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateTermsAndConditionsCategoryMasterCommand(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetTermsAndConditionsCategoryMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetTermsAndConditionsCategoryMasterByIdQuery(id, cancellationToken));

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdateTermsAndConditionsCategoryMasterDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new UpdateTermsAndConditionsCategoryMasterCommand(id, request, cancellationToken));

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteTermsAndConditionsCategoryMasterCommand(id, cancellationToken));

}
