using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.NotificationMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.NotificationMaster;


public record GetNotificationMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetNotificationMastersQueryHandler(INotificationMasterRepository _repository, ICurrentUser _currentUser) : IQueryHandler<GetNotificationMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetNotificationMastersQuery request, CancellationToken cancellationToken)
    {
        request.queryParams.ParentId = _currentUser.UserId;

        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new NotificationMasterDto()
        {
            Id = x.Id,
            Title = x.Title,
            Message = x.Message,
            Type = x.Type,
            ActionUrl = x.ActionUrl,
            IsRead = x.IsRead,
            CreatedAt = x.CreatedAt,
            Since = string.Empty
        }).ToListAsync(cancellationToken);

        foreach (var item in entityListDto)
        {
            if (item?.CreatedAt != null && item.CreatedAt.HasValue)
            {
                item.Since = DateTimeHelper.FormatTimeAgo(item.CreatedAt.Value);
            }
        }

        var pagedResponse = new PagedResponse<NotificationMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
