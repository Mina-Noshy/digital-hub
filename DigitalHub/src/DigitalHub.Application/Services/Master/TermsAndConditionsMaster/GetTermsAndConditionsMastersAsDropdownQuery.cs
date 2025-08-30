using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsMaster;

public record GetTermsAndConditionsMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTermsAndConditionsMastersAsDropdownQueryHandler(ITermsAndConditionsMasterRepository _repository) : IQueryHandler<GetTermsAndConditionsMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTermsAndConditionsMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Description })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}