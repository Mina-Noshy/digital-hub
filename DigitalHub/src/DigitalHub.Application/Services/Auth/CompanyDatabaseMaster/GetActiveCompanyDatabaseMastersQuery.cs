using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;

public record GetActiveCompanyDatabaseMastersQuery
    (CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetActiveCompanyDatabaseMastersQueryHandler(ICompanyDatabaseMasterRepository _repository) : IQueryHandler<GetActiveCompanyDatabaseMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetActiveCompanyDatabaseMastersQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetAllActiveAsync(cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<CompanyDatabaseMasterDto>>();

        return ApiResponse.Success(data: entityListDto);
    }
}
