using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record GetUserProfileQuery
    (CancellationToken cancellationToken = default) : IQuery<ApiResponse>;


public class GetUserProfileQueryHandler(
    IAccountRepository _repository,
    IRoleMasterRepository _roleRepository,
    ICurrentUser _currentUser,
    IPathFinderService _pathFinderService) : IQueryHandler<GetUserProfileQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        long userId = _currentUser.UserId;

        var entity = await _repository.GetUserByIdAsync(userId, cancellationToken);
        if (entity != null)
        {
            var roles = await _roleRepository.GetUserRolesAsync(entity, cancellationToken);

            var entityDto = entity.Adapt<UserMasterDto>();
            entityDto.Roles = roles.ToArray();
            if (!string.IsNullOrWhiteSpace(entity.ProfileImage))
            {
                var folderPath = _pathFinderService.UserProfileFolderPath;
                entityDto.ProfileImageUrl = _pathFinderService.GetFullURL(folderPath, entity.ProfileImage);
            }

            return ApiResponse.Success(data: entityDto);
        }
        return ApiResponse.Failure($"Account with ID '{userId}' was not found.");
    }
}