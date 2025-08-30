using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ITermsAndConditionsMasterRepository : IBaseRepository
{
    IQueryable<TermsAndConditionsMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<TermsAndConditionsMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(TermsAndConditionsMaster entity, CancellationToken cancellationToken);
    IQueryable<TermsAndConditionsMaster> GetPaged(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
