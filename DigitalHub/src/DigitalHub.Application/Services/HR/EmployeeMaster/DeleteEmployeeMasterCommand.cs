using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record DeleteEmployeeMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteEmployeeMasterCommandHandler(IEmployeeMasterRepository _repository) : ICommandHandler<DeleteEmployeeMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteEmployeeMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<EmployeeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
