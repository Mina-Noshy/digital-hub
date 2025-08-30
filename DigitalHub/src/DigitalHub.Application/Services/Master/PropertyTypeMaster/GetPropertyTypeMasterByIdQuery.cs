using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.PropertyTypeMaster;

public record GetPropertyTypeMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPropertyTypeMasterByIdQueryHandler(IPropertyTypeMasterRepository _repository) : IQueryHandler<GetPropertyTypeMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPropertyTypeMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<PropertyTypeMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
