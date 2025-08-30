using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.PropertyTypeMaster;

public record GetPropertyTypeMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPropertyTypeMastersQueryHandler(IPropertyTypeMasterRepository _repository) : IQueryHandler<GetPropertyTypeMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPropertyTypeMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<PropertyTypeMasterDto>>();

        var pagedResponse = new PagedResponse<PropertyTypeMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
