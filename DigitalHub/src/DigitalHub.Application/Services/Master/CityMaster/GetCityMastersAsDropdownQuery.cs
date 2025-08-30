using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Common;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record GetCityMasterAsDropdownQuery
    (DropdownQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCityMasterAsDropdownQueryHandler(ICityMasterRepository _repository) : IQueryHandler<GetCityMasterAsDropdownQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCityMasterAsDropdownQuery request, CancellationToken cancellationToken)
    {
        var dropdownList = await _repository.GetAsDropdown(request.queryParams)
            .Select(x => new DropdownItemDto() { Value = x.Id, Description = x.Name })
            .ToListAsync(cancellationToken);

        return ApiResponse.Success(data: dropdownList);
    }
}