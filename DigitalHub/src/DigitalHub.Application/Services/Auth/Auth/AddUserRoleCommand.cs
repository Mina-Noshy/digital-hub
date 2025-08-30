using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Auth;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.Auth;

public record AddUserRoleCommand
    (UserToRoleDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;

public class AddUserRoleCommandHandler(IAuthRepository _repository,
    IAccountRepository _accountRepository,
    IRoleMasterRepository _roleMasterRepository) : ICommandHandler<AddUserRoleCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.GetUserByIdAsync(request.requestDto.UserId, cancellationToken);
        if (user == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var role = await _roleMasterRepository.GetRoleByNameAsync(request.requestDto.Role, cancellationToken);
        if (role == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var userRoles = await _roleMasterRepository.GetUserRolesAsync(user);
        if (userRoles != null && userRoles.Any(x => x == request.requestDto.Role))
        {
            return ApiResponse.Failure(ApiMessage.ItemAlreadyExist);
        }

        var result = await _repository.AddUserRoleAsync(user, request.requestDto.Role, cancellationToken);
        if (result.Succeeded == false)
        {
            return ApiResponse.Failure(ApiMessage.SomethingWrong);
        }

        return ApiResponse.Success(ApiMessage.SuccessfulCreate);
    }
}