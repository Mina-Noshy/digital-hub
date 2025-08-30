using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.MenuMaster;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.MenuMaster;

public record GetMenuMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetMenuMasterByIdQueryHandler(IMenuMasterRepository _repository) : IQueryHandler<GetMenuMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetMenuMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new MenuMasterDto()
        {
            Id = entity.Id,
            ModuleId = entity.ModuleId,
            Module = entity.GetModule!.Name,
            Icon = entity.Icon,
            Label = entity.Label,
            Name = entity.Name,
            I18nKey = entity.I18nKey,
            SortOrder = entity.SortOrder,
        };

        return ApiResponse.Success(data: entityDto);
    }
}
