using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitCategoryMaster;

public record GetUnitCategoryMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitCategoryMasterByIdQueryHandler(IUnitCategoryMasterRepository _repository) : IQueryHandler<GetUnitCategoryMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitCategoryMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitCategoryMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
