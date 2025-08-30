using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Enums;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class RoleMasterRepository(
    RoleManager<RoleMaster> _roleManager,
    UserManager<UserMaster> _userManager,
    IUnitOfWork _unitOfWork) : IRoleMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public IQueryable<RoleMaster> GetAsDropdown(DropdownQueryParams queryParams)
    {
        return _roleManager.Roles
        .Where(x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || x.Name!.Contains(queryParams.SearchTerm))
        .OrderBy(o => o.Name)
        .Take(queryParams.Limit);
    }
    public async Task<RoleMaster?> GetRoleByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _roleManager.FindByNameAsync(name);
    }
    public async Task<IEnumerable<string>> GetUserRolesAsync(UserMaster user, CancellationToken cancellationToken = default)
    {
        return await _userManager.GetRolesAsync(user);
    }
    public async Task<RoleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return
            await _roleManager.FindByIdAsync(id.ToString());
    }
    public async Task<bool> AddAsync(RoleMaster entity, CancellationToken cancellationToken)
    {
        var role = await _roleManager.CreateAsync(entity);

        return role.Succeeded;
    }
    public async Task<bool> UpdateAsync(RoleMaster entity, CancellationToken cancellationToken)
    {
        var role = await _roleManager.UpdateAsync(entity);

        return role.Succeeded;
    }
    public async Task<bool> DeleteAsync(RoleMaster entity, CancellationToken cancellationToken)
    {
        var role = await _roleManager.DeleteAsync(entity);

        return role.Succeeded;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _roleManager.Roles.CountAsync(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (!string.IsNullOrEmpty(x.Name) && x.Name.Contains(queryParams.SearchTerm)),
            cancellationToken);
    }
    public async Task<IEnumerable<RoleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return await _roleManager.Roles
            .Where(x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.Name + x.NormalizedName).Contains(queryParams.SearchTerm))
            .OrderBy(orderByExpression)
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize)
            .ToListAsync(cancellationToken);
    }
    public IQueryable<ModuleMaster> GetRolePermissions(CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository().GetAll<ModuleMaster>(x =>
            true,
            null,
            nameof(ModuleMaster.GetMenus) +
            "." +
            nameof(MenuMaster.GetPages) +
            "." +
            nameof(PageMaster.GetRolePages));
    }

    public async Task<IEnumerable<RoleMaster>> GetDefaultRolesAsync(UserTypes userType, CancellationToken cancellationToken)
    {
        return
            await _roleManager.Roles.Where(x => x.DefaultFor == userType).ToListAsync(cancellationToken);
    }
}
