using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IRolePageMasterRepository : IBaseRepository
{
    Task<IQueryable<RolePageMaster>> GetUserModulePages(long userId, long moduleId, CancellationToken cancellationToken);
    Task<RolePageMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(RolePageMaster entity, CancellationToken cancellationToken);
    Task<bool> AddRangeAsync(IEnumerable<RolePageMaster> entityList, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(RolePageMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(RolePageMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteRangeAsync(IEnumerable<RolePageMaster> entityList, CancellationToken cancellationToken);
    Task<IEnumerable<RolePageMaster>> GetAllByRoleIdAsync(long roleId, CancellationToken cancellationToken);
    IQueryable<RolePageMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);


}
