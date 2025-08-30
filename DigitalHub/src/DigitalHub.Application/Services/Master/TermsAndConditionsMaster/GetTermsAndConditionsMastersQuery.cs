using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsMaster;

public record GetTermsAndConditionsMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTermsAndConditionsMastersQueryHandler(ITermsAndConditionsMasterRepository _repository) : IQueryHandler<GetTermsAndConditionsMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTermsAndConditionsMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new TermsAndConditionsMasterDto()
        {
            Id = x.Id,
            CategoryId = x.CategoryId,
            Category = x.GetCategory.Name,
            IsRequired = x.IsRequired,
            Description = x.Description,
        }).ToListAsync(cancellationToken);

        var pagedResponse = new PagedResponse<TermsAndConditionsMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
