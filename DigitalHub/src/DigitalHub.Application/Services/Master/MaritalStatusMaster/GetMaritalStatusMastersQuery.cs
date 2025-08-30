using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.MaritalStatusMaster;

public record GetMaritalStatusMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetMaritalStatusMastersQueryHandler(IMaritalStatusMasterRepository _repository) : IQueryHandler<GetMaritalStatusMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetMaritalStatusMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<MaritalStatusMasterDto>>();

        var pagedResponse = new PagedResponse<MaritalStatusMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
