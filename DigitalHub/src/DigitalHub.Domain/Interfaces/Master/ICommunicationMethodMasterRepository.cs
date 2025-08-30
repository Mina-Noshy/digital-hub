using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ICommunicationMethodMasterRepository : IBaseRepository
{
    IQueryable<CommunicationMethodMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<CommunicationMethodMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(CommunicationMethodMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(CommunicationMethodMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(CommunicationMethodMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<CommunicationMethodMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
