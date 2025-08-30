using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.MaritalStatusMaster;

public record GetMaritalStatusMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetMaritalStatusMasterByIdQueryHandler(IMaritalStatusMasterRepository _repository) : IQueryHandler<GetMaritalStatusMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetMaritalStatusMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<MaritalStatusMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
