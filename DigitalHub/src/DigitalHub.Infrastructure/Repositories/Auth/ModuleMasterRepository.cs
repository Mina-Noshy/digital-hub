using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class ModuleMasterRepository(
                                        IUnitOfWork _unitOfWork,
                                        RoleManager<RoleMaster> _roleManager,
                                        UserManager<UserMaster> _userManager
                                    ) : IModuleMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<ModuleMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _unitOfWork.Repository().GetLightCollection<ModuleMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            o => o.OrderBy(x => x.Name),
            1,
            queryParams.Limit);
    }
    public async Task<ModuleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _unitOfWork.Repository().GetByIdAsync<ModuleMaster>(id, cancellationToken);
    }
    public async Task<bool> AddAsync(ModuleMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(ModuleMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(ModuleMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<ModuleMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            cancellationToken);
    }
    public async Task<IEnumerable<ModuleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _unitOfWork.Repository().FindAsync<ModuleMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name.Contains(queryParams.SearchTerm),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            cancellationToken);
    }

    public async Task<IEnumerable<ModuleMaster>> GetUserModules(long userId, CancellationToken cancellationToken)
    {
        // Get the user
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Get the roles for the user
        var roles = await _userManager.GetRolesAsync(user);

        // Get the RoleIds for the roles
        var roleIds = await _roleManager.Roles
            .Where(role => roles.Contains(role.Name ?? string.Empty))
            .Select(role => role.Id)
            .ToListAsync();

        // Join the tables to get the modules
        var modules = _unitOfWork.Repository().GetAll<RolePageMaster>(
            rp => roleIds.Contains(rp.RoleId),
            rp => rp.OrderBy(x => x.GetPage.GetMenu.GetModule!.Name),
            nameof(RolePageMaster.GetPage) +
            "." +
            nameof(RolePageMaster.GetPage.GetMenu) +
            "." +
            nameof(RolePageMaster.GetPage.GetMenu.GetModule));

        return
            await modules.Distinct().Select(x => x.GetPage.GetMenu.GetModule!).ToListAsync(cancellationToken);
    }

}
