using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TitleMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.TitleMaster;

public record GetTitleMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTitleMasterByIdQueryHandler(ITitleMasterRepository _repository) : IQueryHandler<GetTitleMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTitleMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<TitleMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
