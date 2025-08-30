using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record GetEmployeeRoleMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeRoleMastersQueryHandler(IEmployeeRoleMasterRepository _repository) : IQueryHandler<GetEmployeeRoleMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeRoleMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<EmployeeRoleMasterDto>>();

        var pagedResponse = new PagedResponse<EmployeeRoleMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
