using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CountryMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.CountryMaster;

public record GetCountryMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetCountryMasterByIdQueryHandler(ICountryMasterRepository _repository) : IQueryHandler<GetCountryMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetCountryMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<CountryMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
