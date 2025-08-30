using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Auth.Account;

public record GetUsersAsDropdownQuery
(DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUsersAsDropdownQueryHandler(IAccountRepository _repository) : IQueryHandler<GetUsersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUsersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetUsersAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.FirstName + " " + x.LastName })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}