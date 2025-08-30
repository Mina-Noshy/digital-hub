using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Application.Services.Auth.NotificationMaster;

public record MarkRangeNotificationMasterCommand
    (IdsBodyDto request, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class MarkRangeNotificationMasterCommandHandler(INotificationMasterRepository _repository, ICurrentUser _currentUser) : ICommandHandler<MarkRangeNotificationMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(MarkRangeNotificationMasterCommand request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;
        var result = await _repository.MarkRangeAsReadAsync(userId, request.request.IDs, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}