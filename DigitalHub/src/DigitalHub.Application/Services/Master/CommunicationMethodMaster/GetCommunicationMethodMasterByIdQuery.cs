using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.CommunicationMethodMaster;

public record GetCommunicationMethodMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCommunicationMethodMasterByIdQueryHandler(ICommunicationMethodMasterRepository _repository) : IQueryHandler<GetCommunicationMethodMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCommunicationMethodMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<CommunicationMethodMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
