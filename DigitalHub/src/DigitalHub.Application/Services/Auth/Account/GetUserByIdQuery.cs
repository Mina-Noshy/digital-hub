using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record GetUserByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;


public class GetUserByIdQueryHandler(IAccountRepository _repository, IRoleMasterRepository _roleRepository, IPathFinderService _pathFinderService) : IQueryHandler<GetUserByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetUserByIdAsync(request.id, cancellationToken);
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
        return ApiResponse.Failure($"Account with ID '{request.id}' was not found.");
    }
}