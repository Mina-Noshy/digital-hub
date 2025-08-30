using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.HR;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record GetDepartmentMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetDepartmentMastersAsDropdownQueryHandler(IDepartmentMasterRepository _repository) : IQueryHandler<GetDepartmentMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetDepartmentMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Name })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}