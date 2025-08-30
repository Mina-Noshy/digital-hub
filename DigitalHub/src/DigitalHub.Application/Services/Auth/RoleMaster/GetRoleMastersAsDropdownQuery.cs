using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record GetRoleMastersAsDropdownQuery
(DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetRoleMastersAsDropdownQueryHandler(IRoleMasterRepository _repository) : IQueryHandler<GetRoleMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetRoleMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Name! })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}