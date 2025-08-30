using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface IBankMasterRepository : IBaseRepository
{
    IQueryable<BankMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<BankMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(BankMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(BankMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(BankMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<BankMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
