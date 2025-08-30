using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Entities.Auth.Identity;
using DigitalHub.Domain.Enums;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IRoleMasterRepository : IBaseRepository
{
    IQueryable<RoleMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<RoleMaster?> GetRoleByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetUserRolesAsync(UserMaster user, CancellationToken cancellationToken = default);
    Task<RoleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(RoleMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(RoleMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(RoleMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<RoleMaster>> GetDefaultRolesAsync(UserTypes userType, CancellationToken cancellationToken);
    Task<IEnumerable<RoleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    IQueryable<ModuleMaster> GetRolePermissions(CancellationToken cancellationToken);

}
