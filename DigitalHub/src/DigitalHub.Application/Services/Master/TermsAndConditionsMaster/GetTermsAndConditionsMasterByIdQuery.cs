using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;
using DigitalHub.Domain.Interfaces.Master;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsMaster;

public record GetTermsAndConditionsMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTermsAndConditionsMasterByIdQueryHandler(ITermsAndConditionsMasterRepository _repository) : IQueryHandler<GetTermsAndConditionsMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTermsAndConditionsMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new TermsAndConditionsMasterDto()
        {
            Id = entity.Id,
            CategoryId = entity.CategoryId,
            Category = entity.GetCategory.Name,
            IsRequired = entity.IsRequired,
            Description = entity.Description,
        };

        return ApiResponse.Success(data: entityDto);
    }
}
