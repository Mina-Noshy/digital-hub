using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;

namespace DigitalHub.Infrastructure.Repositories.Auth;

internal class RolePageMasterRepository(IUnitOfWork _unitOfWork, UserManager<UserMaster> _userManager) : IRolePageMasterRepository
{
    public IUnitOfWork UnitOfWork => _unitOfWork;

    public async Task<IQueryable<RolePageMaster>> GetUserModulePages(long userId, long moduleId, CancellationToken cancellationToken)
    {
        // Get the user
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new Exception("User not found");
        }

        // Get the roles for the user
        var roles = await _userManager.GetRolesAsync(user);
        if (roles == null || roles.Count == 0)
        {
            throw new Exception("The current user does not have any roles assigned.");
        }

        return _unitOfWork.Repository().GetAll<RolePageMaster>(
            x => x.GetPage.GetMenu.ModuleId == moduleId && !string.IsNullOrEmpty(x.GetRole.Name) && roles.Contains(x.GetRole.Name),
            x => x.OrderBy(x => x.GetPage.GetMenu.Name).ThenBy(x => x.GetPage.Name),
            nameof(RolePageMaster.GetRole),
            nameof(RolePageMaster.GetPage) + "." + nameof(RolePageMaster.GetPage.GetMenu) + "." + nameof(RolePageMaster.GetPage.GetMenu.GetModule));
    }
    public async Task<RolePageMaster?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().GetByIdAsync<RolePageMaster>(
            id,
            cancellationToken,
            nameof(RolePageMaster.GetRole),
            nameof(RolePageMaster.GetPage) + "." + nameof(RolePageMaster.GetPage.GetMenu) + "." + nameof(RolePageMaster.GetPage.GetMenu.GetModule));
    }
    public async Task<bool> AddAsync(RolePageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Add(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> AddRangeAsync(IEnumerable<RolePageMaster> entityList, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository().AddRangeAsync(entityList, cancellationToken);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> UpdateAsync(RolePageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().Update(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteAsync(RolePageMaster entity, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entity);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<bool> DeleteRangeAsync(IEnumerable<RolePageMaster> entityList, CancellationToken cancellationToken)
    {
        _unitOfWork.Repository().SoftDelete(entityList);
        var effectedRows = await _unitOfWork.CommitAsync(cancellationToken);

        return effectedRows > 0;
    }
    public async Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().CountAsync<RolePageMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.GetPage.Name + x.GetRole.Name).Contains(queryParams.SearchTerm),
            cancellationToken);
    }
    public async Task<IEnumerable<RolePageMaster>> GetAllByRoleIdAsync(long roleId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository().GetAllAsync<RolePageMaster>(x => x.RoleId == roleId, null, cancellationToken);
    }
    public IQueryable<RolePageMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken)
    {
        var orderByExpression = queryParams.Ascending ? queryParams.SortColumn : $"{queryParams.SortColumn} DESC";

        return _unitOfWork.Repository().Find<RolePageMaster>(
            x => string.IsNullOrWhiteSpace(queryParams.SearchTerm) || (x.GetPage.Name + x.GetRole.Name).Contains(queryParams.SearchTerm),
            string.IsNullOrWhiteSpace(queryParams.SortColumn) ? null : o => o.OrderBy(orderByExpression),
            queryParams.PageNumber,
            queryParams.PageSize,
            nameof(RolePageMaster.GetRole),
            nameof(RolePageMaster.GetPage) + "." + nameof(RolePageMaster.GetPage.GetMenu) + "." + nameof(RolePageMaster.GetPage.GetMenu.GetModule));
    }




}
