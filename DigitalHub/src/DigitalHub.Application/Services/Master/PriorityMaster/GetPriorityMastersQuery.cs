using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PriorityMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.PriorityMaster;

public record GetPriorityMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPriorityMastersQueryHandler(IPriorityMasterRepository _repository) : IQueryHandler<GetPriorityMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPriorityMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<PriorityMasterDto>>();

        var pagedResponse = new PagedResponse<PriorityMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
