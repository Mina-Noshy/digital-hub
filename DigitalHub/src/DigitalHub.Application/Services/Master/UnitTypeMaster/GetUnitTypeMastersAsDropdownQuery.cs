using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.UnitTypeMaster;

public record GetUnitTypeMastersAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetUnitTypeMastersAsDropdownQueryHandler(IUnitTypeMasterRepository _repository) : IQueryHandler<GetUnitTypeMastersAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUnitTypeMastersAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
           .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Name })
           .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}