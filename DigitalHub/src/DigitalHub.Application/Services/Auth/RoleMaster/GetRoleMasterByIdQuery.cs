using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RoleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record GetRoleMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetRoleMasterByIdQueryHandler(IRoleMasterRepository _repository) : IQueryHandler<GetRoleMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetRoleMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<RoleMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
