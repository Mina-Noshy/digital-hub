using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.Auth;

public record GetUserModulePagesQuery(long moduleId, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;



public class GetUserModulePagesQueryHandler(IRolePageMasterRepository _rolePageMasterRepository, ICurrentUser _currentUser) : IQueryHandler<GetUserModulePagesQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUserModulePagesQuery request, CancellationToken cancellationToken)
    {
        // Try to get current user id
        var userId = _currentUser.UserId;

        var entityList = await _rolePageMasterRepository.GetUserModulePages(userId, request.moduleId, cancellationToken);

        var entityListDto = await entityList
            .GroupBy(x => x.GetPage.GetMenu) // Group by Menu
            .Select(menuGroup => new UserModuleMenuDto()
            {
                Id = menuGroup.Key.Id,
                Icon = menuGroup.Key.Icon,
                Label = menuGroup.Key.Label,
                Name = menuGroup.Key.Name,
                I18nKey = menuGroup.Key.I18nKey,
                SortOrder = menuGroup.Key.SortOrder,

                // Merge duplicate pages by Id and aggregate permissions
                Pages = menuGroup
                    .GroupBy(page => page.GetPage.Id) // Group by Page Id
                    .Select(g => new UserModuleMenuPageDto()
                    {
                        Id = g.Key,
                        Name = g.First().GetPage.Name,
                        Label = g.First().GetPage.Label,
                        Icon = g.First().GetPage.Icon,
                        Path = g.First().GetPage.Path,
                        I18nKey = g.First().GetPage.I18nKey,
                        SortOrder = g.First().GetPage.SortOrder,

                        // Merge permissions using logical OR (||)
                        Create = g.Any(p => p.Create),
                        Update = g.Any(p => p.Update),
                        Delete = g.Any(p => p.Delete),
                        Export = g.Any(p => p.Export),
                        Print = g.Any(p => p.Print)
                    })
                    .OrderBy(x => x.SortOrder)
                    .ToList()
            })
            .OrderBy(x => x.SortOrder)
            .ToListAsync(cancellationToken);


        return ApiResponse.Success(data: entityListDto);
    }


}
