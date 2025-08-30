using DigitalHub.Domain.Entities.Master;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.Interfaces.Master;

public interface ITermsAndConditionsCategoryMasterRepository : IBaseRepository
{
    IQueryable<TermsAndConditionsCategoryMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<TermsAndConditionsCategoryMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> AddAsync(TermsAndConditionsCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(TermsAndConditionsCategoryMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(TermsAndConditionsCategoryMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<TermsAndConditionsCategoryMaster>> GetPagedAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(PaginationQueryParams queryParams, CancellationToken cancellationToken);
}
