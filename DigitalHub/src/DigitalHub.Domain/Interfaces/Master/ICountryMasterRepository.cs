using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ICountryMasterRepository : IBaseRepository
{
    IQueryable<CountryMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<CountryMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(CountryMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CountryMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(CountryMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<CountryMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
