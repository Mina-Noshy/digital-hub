using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RoleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record DeleteRoleMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteRoleMasterCommandHandler(IRoleMasterRepository _repository) : ICommandHandler<DeleteRoleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteRoleMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<RoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
