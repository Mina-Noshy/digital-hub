using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Application.Services.Auth.Account;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "account")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
public class AccountsController : BaseApiController
{
    [HttpGet("user/dropdown")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUsersAsDropdown([FromQuery] DropdownQueryParams queryParams, CancellationToken cancellationToken)
       => await Mediator.Send(new GetUsersAsDropdownQuery(queryParams, cancellationToken));

    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(CreateUserDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new CreateUserCommand(request, cancellationToken));

    [HttpPut("user/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateUser(long id, [FromBody] UpdateUserDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserCommand(id, request, cancellationToken));

    [HttpGet("user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUsers([FromQuery] PaginationQueryParams queryParams, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUsersQuery(queryParams, cancellationToken));

    [HttpGet("user/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserByIdQuery(id, cancellationToken));

    [HttpDelete("user/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteUser(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new DeleteUserCommand(id, cancellationToken));

    [HttpGet("user-profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetUserProfile(CancellationToken cancellationToken)
        => await Mediator.Send(new GetUserProfileQuery(cancellationToken));

    [HttpPut("user-profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserProfileDto request, CancellationToken cancellationToken)
            => await Mediator.Send(new UpdateUserProfileCommand(request, cancellationToken));





    [HttpPost("toggel-contact-number-confirmation/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ToggleContactNumberConfirmation(long id, CancellationToken cancellationToken)
            => await Mediator.Send(new ToggleContactNumberConfirmationCommand(id, cancellationToken));

    [HttpPost("toggel-email-confirmation/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ToggleEmailConfirmation(long id, CancellationToken cancellationToken)
            => await Mediator.Send(new ToggleEmailConfirmationCommand(id, cancellationToken));

    [HttpPost("toggel-user-active-status/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ToggleUserActiveStatus(long id, CancellationToken cancellationToken)
            => await Mediator.Send(new ToggleUserActiveStatusCommand(id, cancellationToken));

    [HttpPost("toggel-user-block-status/{id:long}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ToggleUserBlockStatus(long id, CancellationToken cancellationToken)
            => await Mediator.Send(new ToggleUserBlockStatusCommand(id, cancellationToken));









    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto request, CancellationToken cancellationToken)
        => await Mediator.Send(new ConfirmEmailCommand(request, cancellationToken));

    [HttpPost("send-confirmation-email")]
    public async Task<IActionResult> SendConfirmationEmail(string email, CancellationToken cancellationToken)
        => await Mediator.Send(new SendConfirmationEmailCommand(email, cancellationToken));

}
