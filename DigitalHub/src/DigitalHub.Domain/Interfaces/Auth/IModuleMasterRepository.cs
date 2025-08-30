using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IModuleMasterRepository : IBaseRepository
{
    IQueryable<ModuleMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<ModuleMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(ModuleMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ModuleMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(ModuleMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<ModuleMaster>> GetUserModules(long userId, CancellationToken cancellationToken);
    Task<IEnumerable<ModuleMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
