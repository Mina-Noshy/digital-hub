using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CityMaster;
using DigitalHub.Domain.Interfaces.Master;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record GetCityMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCityMasterByIdQueryHandler(ICityMasterRepository _repository) : IQueryHandler<GetCityMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCityMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new CityMasterDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            CountryId = entity.CountryId,
            Country = entity.GetCountry.Name
        };

        return ApiResponse.Success(data: entityDto);
    }
}
