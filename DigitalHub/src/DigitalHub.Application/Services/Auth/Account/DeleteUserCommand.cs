using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record DeleteUserCommand
(long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteUserCommandHandler(IAccountRepository _repository) : ICommandHandler<DeleteUserCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetUserByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteUserAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UserMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
