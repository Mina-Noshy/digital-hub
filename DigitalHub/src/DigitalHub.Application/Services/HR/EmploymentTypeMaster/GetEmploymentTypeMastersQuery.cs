using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmploymentTypeMaster;

public record GetEmploymentTypeMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmploymentTypeMastersQueryHandler(IEmploymentTypeMasterRepository _repository) : IQueryHandler<GetEmploymentTypeMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmploymentTypeMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<EmploymentTypeMasterDto>>();

        var pagedResponse = new PagedResponse<EmploymentTypeMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
