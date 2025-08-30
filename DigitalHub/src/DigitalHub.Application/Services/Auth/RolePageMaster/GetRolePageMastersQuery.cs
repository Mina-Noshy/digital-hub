using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.RolePageMaster;

public record GetRolePageMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetRolePageMastersQueryHandler(IRolePageMasterRepository _repository) : IQueryHandler<GetRolePageMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetRolePageMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new RolePageMasterDto()
        {
            Id = x.Id,
            RoleId = x.RoleId,
            PageId = x.PageId,
            Role = x.GetRole.Name,
            Page = x.GetPage.Name,
            Menu = x.GetPage.GetMenu.Name,
            Module = x.GetPage.GetMenu.GetModule!.Name,
            Create = x.Create,
            Delete = x.Delete,
            Export = x.Export,
            Print = x.Print,
            Update = x.Update
        }).ToListAsync(cancellationToken);

        var pagedResponse = new PagedResponse<RolePageMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
