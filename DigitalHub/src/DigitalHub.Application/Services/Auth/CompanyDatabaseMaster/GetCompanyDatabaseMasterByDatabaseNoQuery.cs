using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;

public record GetCompanyDatabaseMasterByDatabaseNoQuery
    (string databaseNo, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCompanyDatabaseMasterByDatabaseNoQueryHandler(ICompanyDatabaseMasterRepository _repository) : IQueryHandler<GetCompanyDatabaseMasterByDatabaseNoQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCompanyDatabaseMasterByDatabaseNoQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetByDatabaseNoAsync(request.databaseNo, cancellationToken);

        var entityListDto = entityList.Adapt<CompanyDatabaseMasterDto>();

        return ApiResponse.Success(data: entityListDto);
    }
}
