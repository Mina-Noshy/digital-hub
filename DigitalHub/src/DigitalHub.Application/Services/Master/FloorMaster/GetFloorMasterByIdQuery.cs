using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.FloorMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.FloorMaster;

public record GetFloorMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetFloorMasterByIdQueryHandler(IFloorMasterRepository _repository) : IQueryHandler<GetFloorMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetFloorMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<FloorMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
