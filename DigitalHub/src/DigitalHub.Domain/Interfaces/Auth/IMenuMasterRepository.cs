using DigitalHub.Domain.Entities.Auth;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Auth;

public interface IMenuMasterRepository : IBaseRepository
{
    IQueryable<MenuMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<MenuMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(MenuMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(MenuMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(MenuMaster entity, CancellationToken cancellationToken);
    IQueryable<MenuMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
