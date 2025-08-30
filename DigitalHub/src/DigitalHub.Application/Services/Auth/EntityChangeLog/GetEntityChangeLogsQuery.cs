using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.EntityChangeLog;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Auth.EntityChangeLog;


public record GetEntityChangeLogsQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEntityChangeLogsQueryHandler(IEntityChangeLogRepository _repository) : IQueryHandler<GetEntityChangeLogsQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEntityChangeLogsQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<EntityChangeLogDto>>();

        var pagedResponse = new PagedResponse<EntityChangeLogDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
