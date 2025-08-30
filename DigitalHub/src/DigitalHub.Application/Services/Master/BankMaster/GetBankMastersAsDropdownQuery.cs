using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.BankMaster;

public record GetBankMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetBankMastersAsDropdownQueryHandler(IBankMasterRepository _repository) : IQueryHandler<GetBankMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetBankMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
            .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.BankName })
            .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}