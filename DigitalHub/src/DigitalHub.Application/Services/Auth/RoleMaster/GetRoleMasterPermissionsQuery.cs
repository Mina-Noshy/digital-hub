using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RoleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record GetRoleMasterPermissionsQuery
    (long roleId, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetRoleMasterPermissionsQueryHandler(IRoleMasterRepository _repository) : IQueryHandler<GetRoleMasterPermissionsQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetRoleMasterPermissionsQuery request, CancellationToken cancellationToken)
    {
        var entityList = _repository.GetRolePermissions(cancellationToken);


        var entityListDto = await entityList
            .Where(m => m.GetMenus != null) // Ensure the module has menus
            .Select(m => new ModuleForRolePermissionDto
            {
                Id = m.Id,
                Name = m.I18nKey ?? m.Label,
                Menus = m.GetMenus!
                    .Where(menu => menu.GetPages != null) // Ensure the menu has pages
                    .Select(menu => new MenuForRolePermissionDto
                    {
                        Id = menu.Id,
                        Name = menu.I18nKey ?? menu.Label,
                        Pages = menu.GetPages!
                            .Select(page => new PageForRolePermissionDto
                            {
                                Id = page.Id,
                                Name = page.I18nKey ?? page.Label,
                                Selected = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id),
                                Create = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id && rolePage.Create),
                                Update = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id && rolePage.Update),
                                Delete = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id && rolePage.Delete),
                                Export = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id && rolePage.Export),
                                Print = page.GetRolePages!.Any(rolePage => rolePage.RoleId == request.roleId && rolePage.PageId == page.Id && rolePage.Print),
                            })
                    })
            }).ToListAsync(cancellationToken);

        return await Task.FromResult(ApiResponse.Success(data: entityListDto));
    }
}
