using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common.SingleValue;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;

namespace DigitalHub.Application.Services.Auth.NotificationMaster;


public record GetUnReadNotificationMasterCountQuery
    (CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnReadNotificationMasterCountQueryHandler(INotificationMasterRepository _repository, ICurrentUser _currentUser) : IQueryHandler<GetUnReadNotificationMasterCountQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnReadNotificationMasterCountQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        var totalCount = await _repository.UnReadCountAsync(userId, cancellationToken);

        var response = new Int32Dto() { Value = totalCount };

        return ApiResponse.Success(data: response);
    }
}
