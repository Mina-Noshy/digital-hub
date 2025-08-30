using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record GetEmployeeMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeMastersAsDropdownQueryHandler(IEmployeeMasterRepository _repository) : IQueryHandler<GetEmployeeMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Title + " " + x.FirstName + " " + x.LastName })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}