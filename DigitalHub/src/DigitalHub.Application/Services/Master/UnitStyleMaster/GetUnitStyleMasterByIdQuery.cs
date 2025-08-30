using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitStyleMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.UnitStyleMaster;

public record GetUnitStyleMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitStyleMasterByIdQueryHandler(IUnitStyleMasterRepository _repository) : IQueryHandler<GetUnitStyleMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitStyleMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<UnitStyleMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
