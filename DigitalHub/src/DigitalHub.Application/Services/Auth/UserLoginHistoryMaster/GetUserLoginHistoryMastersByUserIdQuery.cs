using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.UserLoginHistoryMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.UserLoginHistoryMaster;


public record GetUserLoginHistoryMastersByUserIdQuery
    (long userId, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUserLoginHistoryMastersByUserIdQueryHandler(IUserLoginHistoryMasterRepository _repository) : IQueryHandler<GetUserLoginHistoryMastersByUserIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUserLoginHistoryMastersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var entityList = await _repository.GetByUserIdAsync(request.userId, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<UserLoginHistoryMasterDto>>();

        return ApiResponse.Success(data: entityListDto);
    }
}
