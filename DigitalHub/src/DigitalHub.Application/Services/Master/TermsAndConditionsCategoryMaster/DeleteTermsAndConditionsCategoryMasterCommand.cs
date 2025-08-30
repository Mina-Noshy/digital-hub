using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;

public record DeleteTermsAndConditionsCategoryMasterCommand
    (long id, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class DeleteTermsAndConditionsCategoryMasterCommandHandler(ITermsAndConditionsCategoryMasterRepository _repository) : ICommandHandler<DeleteTermsAndConditionsCategoryMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(DeleteTermsAndConditionsCategoryMasterCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var result = await _repository.DeleteAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<TermsAndConditionsCategoryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulDelete, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedDelete);
    }
}
