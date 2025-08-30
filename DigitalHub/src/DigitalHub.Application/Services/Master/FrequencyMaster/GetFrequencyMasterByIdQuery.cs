using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.FrequencyMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.FrequencyMaster;

public record GetFrequencyMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetFrequencyMasterByIdQueryHandler(IFrequencyMasterRepository _repository) : IQueryHandler<GetFrequencyMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetFrequencyMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<FrequencyMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
