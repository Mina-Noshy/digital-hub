using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitModelMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitModelMaster;

public record DeleteUnitModelMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteUnitModelMasterCommandHandler(IUnitModelMasterRepository _repository) : ICommandHandler<DeleteUnitModelMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteUnitModelMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitModelMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
