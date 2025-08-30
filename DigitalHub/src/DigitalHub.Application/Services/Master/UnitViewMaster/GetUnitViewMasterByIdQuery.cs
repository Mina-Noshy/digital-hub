using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitViewMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitViewMaster;

public record GetUnitViewMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitViewMasterByIdQueryHandler(IUnitViewMasterRepository _repository) : IQueryHandler<GetUnitViewMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitViewMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitViewMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
