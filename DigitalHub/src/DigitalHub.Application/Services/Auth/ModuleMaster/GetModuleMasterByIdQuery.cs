using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.ModuleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Mapster;

namespace DigitalHub.Application.Services.Auth.ModuleMaster;

public record GetModuleMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetModuleMasterByIdQueryHandler(IModuleMasterRepository _repository) : IQueryHandler<GetModuleMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetModuleMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<ModuleMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
