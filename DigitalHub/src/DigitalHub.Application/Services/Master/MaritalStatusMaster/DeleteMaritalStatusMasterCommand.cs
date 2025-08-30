using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.MaritalStatusMaster;

public record DeleteMaritalStatusMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteMaritalStatusMasterCommandHandler(IMaritalStatusMasterRepository _repository) : ICommandHandler<DeleteMaritalStatusMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteMaritalStatusMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<MaritalStatusMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
