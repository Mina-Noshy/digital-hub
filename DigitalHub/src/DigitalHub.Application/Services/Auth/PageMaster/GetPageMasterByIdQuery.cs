using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.PageMaster;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.PageMaster;

public record GetPageMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPageMasterByIdQueryHandler(IPageMasterRepository _repository) : IQueryHandler<GetPageMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPageMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new PageMasterDto()
        {
            Id = entity.Id,
            MenuId = entity.MenuId,
            Menu = entity.GetMenu.Name,
            Module = entity.GetMenu.GetModule!.Name,
            Name = entity.Name,
            Icon = entity.Icon,
            Label = entity.Label,
            Path = entity.Path,
            I18nKey = entity.I18nKey,
            SortOrder = entity.SortOrder,
        };

        return ApiResponse.Success(data: entityDto);
    }
}
