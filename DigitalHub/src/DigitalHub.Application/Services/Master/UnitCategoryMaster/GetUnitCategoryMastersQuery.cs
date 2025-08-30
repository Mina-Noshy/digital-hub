using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitCategoryMaster;

public record GetUnitCategoryMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitCategoryMastersQueryHandler(IUnitCategoryMasterRepository _repository) : IQueryHandler<GetUnitCategoryMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitCategoryMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<UnitCategoryMasterDto>>();

        var pagedResponse = new PagedResponse<UnitCategoryMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
