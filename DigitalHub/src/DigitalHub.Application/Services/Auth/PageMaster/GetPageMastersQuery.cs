using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.PageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.PageMaster;

public record GetPageMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetPageMastersQueryHandler(IPageMasterRepository _repository) : IQueryHandler<GetPageMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetPageMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new PageMasterDto()
        {
            Id = x.Id,
            MenuId = x.MenuId,
            Menu = x.GetMenu.Name,
            Module = x.GetMenu.GetModule!.Name,
            Name = x.Name,
            Icon = x.Icon,
            Label = x.Label,
            Path = x.Path,
            I18nKey = x.I18nKey,
            SortOrder = x.SortOrder,
        }).ToListAsync(cancellationToken);

        var pagedResponse = new PagedResponse<PageMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
