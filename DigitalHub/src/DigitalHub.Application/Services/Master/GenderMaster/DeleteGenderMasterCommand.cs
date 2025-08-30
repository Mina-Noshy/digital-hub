using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.GenderMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.GenderMaster;

public record DeleteGenderMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteGenderMasterCommandHandler(IGenderMasterRepository _repository) : ICommandHandler<DeleteGenderMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteGenderMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<GenderMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
