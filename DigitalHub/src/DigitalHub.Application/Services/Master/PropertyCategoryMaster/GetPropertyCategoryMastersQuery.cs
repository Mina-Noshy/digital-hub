using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.PropertyCategoryMaster;

public record GetPropertyCategoryMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPropertyCategoryMastersQueryHandler(IPropertyCategoryMasterRepository _repository) : IQueryHandler<GetPropertyCategoryMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPropertyCategoryMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<PropertyCategoryMasterDto>>();

        var pagedResponse = new PagedResponse<PropertyCategoryMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
