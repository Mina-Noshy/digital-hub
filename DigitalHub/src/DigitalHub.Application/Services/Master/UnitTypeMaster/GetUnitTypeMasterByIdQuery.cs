using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitTypeMaster;

public record GetUnitTypeMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitTypeMasterByIdQueryHandler(IUnitTypeMasterRepository _repository) : IQueryHandler<GetUnitTypeMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitTypeMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitTypeMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
