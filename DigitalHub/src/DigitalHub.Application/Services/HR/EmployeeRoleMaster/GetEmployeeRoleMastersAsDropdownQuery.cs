using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record GetEmployeeRoleMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeRoleMastersAsDropdownQueryHandler(IEmployeeRoleMasterRepository _repository) : IQueryHandler<GetEmployeeRoleMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeRoleMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Name })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}