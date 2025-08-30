using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.CommunicationMethodMaster;

public record GetCommunicationMethodMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCommunicationMethodMastersQueryHandler(ICommunicationMethodMasterRepository _repository) : IQueryHandler<GetCommunicationMethodMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCommunicationMethodMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<CommunicationMethodMasterDto>>();

        var pagedResponse = new PagedResponse<CommunicationMethodMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
