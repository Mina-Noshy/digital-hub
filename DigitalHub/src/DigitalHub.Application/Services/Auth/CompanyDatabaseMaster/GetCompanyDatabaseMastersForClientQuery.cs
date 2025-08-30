using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;


public record GetCompanyDatabaseMastersForClientQuery
    (string companyKey, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCompanyDatabaseMastersForClientQueryHandler(ICompanyDatabaseMasterRepository _repository) : IQueryHandler<GetCompanyDatabaseMastersForClientQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCompanyDatabaseMastersForClientQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetAllForClientAsync(request.companyKey, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<CompanyDatabaseMasterDto>>();

        return ApiResponse.Success(data: entityListDto);
    }
}
