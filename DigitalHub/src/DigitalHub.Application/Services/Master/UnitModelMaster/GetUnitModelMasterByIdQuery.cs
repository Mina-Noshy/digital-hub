using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitModelMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitModelMaster;

public record GetUnitModelMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitModelMasterByIdQueryHandler(IUnitModelMasterRepository _repository) : IQueryHandler<GetUnitModelMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitModelMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitModelMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
