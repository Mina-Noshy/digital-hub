using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitClassMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitClassMaster;

public record GetUnitClassMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitClassMasterByIdQueryHandler(IUnitClassMasterRepository _repository) : IQueryHandler<GetUnitClassMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitClassMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitClassMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
