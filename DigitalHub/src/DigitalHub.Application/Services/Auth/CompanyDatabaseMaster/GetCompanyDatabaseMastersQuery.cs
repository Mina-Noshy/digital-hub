using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;

public record GetCompanyDatabaseMastersQuery
    (CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCompanyDatabaseMastersQueryHandler(ICompanyDatabaseMasterRepository _repository) : IQueryHandler<GetCompanyDatabaseMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCompanyDatabaseMastersQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetAllAsync(cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<CompanyDatabaseMasterDto>>();

        return ApiResponse.Success(data: entityListDto);
    }
}
