using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CityMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record DeleteCityMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteCityMasterCommandHandler(ICityMasterRepository _repository) : ICommandHandler<DeleteCityMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteCityMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<CityMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
