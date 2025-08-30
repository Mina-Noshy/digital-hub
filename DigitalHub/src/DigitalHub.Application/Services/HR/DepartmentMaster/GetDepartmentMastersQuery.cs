using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.DepartmentMaster;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record GetDepartmentMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetDepartmentMastersQueryHandler(IDepartmentMasterRepository _repository) : IQueryHandler<GetDepartmentMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetDepartmentMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<DepartmentMasterDto>>();

        var pagedResponse = new PagedResponse<DepartmentMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
