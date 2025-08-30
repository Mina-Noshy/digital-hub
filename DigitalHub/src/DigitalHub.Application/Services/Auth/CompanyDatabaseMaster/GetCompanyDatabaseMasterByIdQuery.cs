using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.CompanyDatabaseMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.CompanyDatabaseMaster;

public record GetCompanyDatabaseMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCompanyDatabaseMasterByIdQueryHandler(ICompanyDatabaseMasterRepository _repository) : IQueryHandler<GetCompanyDatabaseMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCompanyDatabaseMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetByIdAsync(request.id, cancellationToken);

        var entityListDto = entityList.Adapt<CompanyDatabaseMasterDto>();

        return ApiResponse.Success(data: entityListDto);
    }
}
