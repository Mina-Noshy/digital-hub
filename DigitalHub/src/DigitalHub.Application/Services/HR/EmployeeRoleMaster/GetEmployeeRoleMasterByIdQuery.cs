using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record GetEmployeeRoleMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmployeeRoleMasterByIdQueryHandler(IEmployeeRoleMasterRepository _repository) : IQueryHandler<GetEmployeeRoleMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmployeeRoleMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<EmployeeRoleMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
