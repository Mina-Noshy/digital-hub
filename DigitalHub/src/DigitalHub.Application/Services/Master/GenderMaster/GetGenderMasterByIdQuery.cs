using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.GenderMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.GenderMaster;

public record GetGenderMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetGenderMasterByIdQueryHandler(IGenderMasterRepository _repository) : IQueryHandler<GetGenderMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetGenderMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<GenderMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
