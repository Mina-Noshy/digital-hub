using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Application.Services.Auth.NotificationMaster;


public record DeleteRangeNotificationMasterCommand
    (IdsBodyDto request, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class DeleteRangeNotificationMasterCommandHandler(INotificationMasterRepository _repository, ICurrentUser _currentUser) : ICommandHandler<DeleteRangeNotificationMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(DeleteRangeNotificationMasterCommand request, CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;
        var result = await _repository.DeleteRangeAsync(userId, request.request.IDs, cancellationToken);
        if (result)
        {
            return ApiResponse.Success(ApiMessage.SuccessfulDelete);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}