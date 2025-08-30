using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;

public record GetTermsAndConditionsCategoryMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetTermsAndConditionsCategoryMasterByIdQueryHandler(ITermsAndConditionsCategoryMasterRepository _repository) : IQueryHandler<GetTermsAndConditionsCategoryMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetTermsAndConditionsCategoryMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<TermsAndConditionsCategoryMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
