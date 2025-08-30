using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CityMaster;
using DigitalHub.Domain.Interfaces.Master;
using DigitalHub.Domain.QueryParams.Common;
using Microsoft.EntityFrameworkCore;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record GetCityMastersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCityMastersQueryHandler(ICityMasterRepository _repository) : IQueryHandler<GetCityMastersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCityMastersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.CountAsync(request.queryParams, cancellationToken);

        var entityList = _repository.GetPaged(request.queryParams, cancellationToken);

        var entityListDto = await entityList.Select(x => new CityMasterDto()
        {
            Id = x.Id,
            Name = x.Name,
            CountryId = x.CountryId,
            Country = x.GetCountry.Name
        }).ToListAsync(cancellationToken);

        var pagedResponse = new PagedResponse<CityMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}
