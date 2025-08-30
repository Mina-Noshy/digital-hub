using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.MenuMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.MenuMaster;

public record GetMenuMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetMenuMastersQueryHandler(IMenuMasterRepository _repository) : IQueryHandler<GetMenuMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetMenuMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new MenuMasterDto()
        {
            Id = x.Id,
            ModuleId = x.ModuleId,
            Module = x.GetModule!.Name,
            Name = x.Name,
            Label = x.Label,
            Icon = x.Icon,
            I18nKey = x.I18nKey,
            SortOrder = x.SortOrder,
        }).ToListAsync(cancellationToken);

        var pagedResponse = new PagedResponse<MenuMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
