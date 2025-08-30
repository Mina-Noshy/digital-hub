using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;
using DigitalHub.Domain.Interfaces.HR;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmploymentTypeMaster;

public record GetEmploymentTypeMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetEmploymentTypeMasterByIdQueryHandler(IEmploymentTypeMasterRepository _repository) : IQueryHandler<GetEmploymentTypeMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetEmploymentTypeMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<EmploymentTypeMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
