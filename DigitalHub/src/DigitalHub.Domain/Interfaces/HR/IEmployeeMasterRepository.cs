using DigitalHub.Domain.Entities.HR;
using DigitalHub.Domain.Interfaces.Common.Context;
using DigitalHub.Domain.QueryParams.Common;
using DigitalHub.Domain.QueryParams.HR;

namespace DigitalHub.Domain.Interfaces.HR;

public interface IEmployeeMasterRepository : IBaseRepository
{
    IQueryable<EmployeeMaster> GetAsDropdown(DropdownQueryParams queryParams);
    Task<string> GetNextEmployeeCodeAsync(CancellationToken cancellationToken);
    Task<EmployeeMaster?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<EmployeeMaster?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<EmployeeMaster?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<bool> AddAsync(EmployeeMaster entity, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(EmployeeMaster entity, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(EmployeeMaster entity, CancellationToken cancellationToken);
    Task<IEnumerable<EmployeeMaster>> GetPagedAsync(EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken);
    Task<int> CountAsync(EmployeeMasterQueryParams queryParams, CancellationToken cancellationToken);
}
