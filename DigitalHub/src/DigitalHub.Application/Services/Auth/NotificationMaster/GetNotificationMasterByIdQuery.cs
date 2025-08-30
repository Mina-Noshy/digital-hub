using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.NotificationMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.NotificationMaster;

public record GetNotificationMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetNotificationMasterByIdQueryHandler(INotificationMasterRepository _repository) : IQueryHandler<GetNotificationMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetNotificationMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<NotificationMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
