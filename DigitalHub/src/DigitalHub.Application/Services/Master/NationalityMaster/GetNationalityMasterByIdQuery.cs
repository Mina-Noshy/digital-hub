using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.NationalityMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.NationalityMaster;

public record GetNationalityMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetNationalityMasterByIdQueryHandler(INationalityMasterRepository _repository) : IQueryHandler<GetNationalityMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetNationalityMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<NationalityMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
