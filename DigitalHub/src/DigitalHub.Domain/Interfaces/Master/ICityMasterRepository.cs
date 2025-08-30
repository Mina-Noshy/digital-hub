using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ICityMasterRepository : IBaseRepository
{
    IQueryable<CityMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<CityMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(CityMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CityMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(CityMaster entity, CancellationToken cancellationToken);
    IQueryable<CityMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
