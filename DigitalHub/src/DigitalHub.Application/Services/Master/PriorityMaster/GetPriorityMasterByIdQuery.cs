using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PriorityMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.PriorityMaster;

public record GetPriorityMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPriorityMasterByIdQueryHandler(IPriorityMasterRepository _repository) : IQueryHandler<GetPriorityMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPriorityMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<PriorityMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
