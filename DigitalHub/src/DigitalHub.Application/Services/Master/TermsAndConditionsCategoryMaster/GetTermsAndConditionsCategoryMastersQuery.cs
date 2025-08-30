using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;

public record GetTermsAndConditionsCategoryMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTermsAndConditionsCategoryMastersQueryHandler(ITermsAndConditionsCategoryMasterRepository _repository) : IQueryHandler<GetTermsAndConditionsCategoryMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTermsAndConditionsCategoryMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<TermsAndConditionsCategoryMasterDto>>();

        var pagedResponse = new PagedResponse<TermsAndConditionsCategoryMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
