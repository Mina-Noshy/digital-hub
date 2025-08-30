using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.DepartmentMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record DeleteDepartmentMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteDepartmentMasterCommandHandler(IDepartmentMasterRepository _repository) : ICommandHandler<DeleteDepartmentMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteDepartmentMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<DepartmentMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
