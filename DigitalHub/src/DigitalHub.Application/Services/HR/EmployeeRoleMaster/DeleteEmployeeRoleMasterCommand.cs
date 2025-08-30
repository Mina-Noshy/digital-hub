using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record DeleteEmployeeRoleMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteEmployeeRoleMasterCommandHandler(IEmployeeRoleMasterRepository _repository) : ICommandHandler<DeleteEmployeeRoleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteEmployeeRoleMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<EmployeeRoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
