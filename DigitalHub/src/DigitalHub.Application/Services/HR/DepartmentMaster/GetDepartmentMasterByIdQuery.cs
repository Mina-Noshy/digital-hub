using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.DepartmentMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record GetDepartmentMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetDepartmentMasterByIdQueryHandler(IDepartmentMasterRepository _repository) : IQueryHandler<GetDepartmentMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetDepartmentMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<DepartmentMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
