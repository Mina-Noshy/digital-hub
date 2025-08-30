using DigitalHub.Domain.QueryParams.Common;

namespace DigitalHub.Domain.QueryParams.HR;

public record EmployeeMasterQueryParams : PaginationQueryParams
{
    public long DepartmentId { get; set; } = 0;
    public long RoleId { get; set; } = 0;
    public long EmploymentTypeId { get; set; } = 0;
}
