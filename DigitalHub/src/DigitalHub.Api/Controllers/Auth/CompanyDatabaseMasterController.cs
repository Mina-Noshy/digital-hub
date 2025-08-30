using Asp.Versioning;
using DigitalHub.Api.Controllers.Base;
using DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;
using Microsoft.AspNetCore.Mvc;

namespace DigitalHub.Api.Controllers.Auth;

[Route(ApiVersionsConfig.Api.Routes.Auth + "company-database-master")]
[ApiVersion(ApiVersionsConfig.Api.Versions.V_1_0)]
public class CompanyDatabaseMasterController : BaseApiController
{
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
        => await Mediator.Send(new GetCompanyDatabaseMasterByIdQuery(id, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => await Mediator.Send(new GetCompanyDatabaseMastersQuery(cancellationToken));

    [HttpGet("all-active")]
    public async Task<IActionResult> GetAllActive(CancellationToken cancellationToken)
        => await Mediator.Send(new GetActiveCompanyDatabaseMastersQuery(cancellationToken));

    [HttpGet("all-for-customers")]
    public async Task<IActionResult> GetAllForCustomers(string companyKey, CancellationToken cancellationToken)
        => await Mediator.Send(new GetCompanyDatabaseMastersForClientQuery(companyKey, cancellationToken));

    [HttpGet("by-database-no")]
    public async Task<IActionResult> GetByDatabaseNo(string databaseNo, CancellationToken cancellationToken)
        => await Mediator.Send(new GetCompanyDatabaseMasterByDatabaseNoQuery(databaseNo, cancellationToken));
}
