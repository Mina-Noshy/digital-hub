using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Application.Services.Auth.NotificationMaster;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "notification-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class NotificationMasterController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetNotificationMastersQuery(queryParams, cancellationToken));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetNotificationMasterByIdQuery(id, cancellationToken));

    [HttpPut("mark-read")]
    public async Task<IActionResult> MarkAsRead([FromBody] IdsBodyDto request, CancellationToken cancellationToken)
       => await Mediator.Send(new MarkRangeNotificationMasterCommand(request, cancellationToken));

    [HttpPost("delete-range")]
    public async Task<IActionResult> DeleteRange([FromBody] IdsBodyDto request, CancellationToken cancellationToken)
       => await Mediator.Send(new DeleteRangeNotificationMasterCommand(request, cancellationToken));

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnReadCount(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUnReadNotificationMasterCountQuery(cancellationToken));
}
