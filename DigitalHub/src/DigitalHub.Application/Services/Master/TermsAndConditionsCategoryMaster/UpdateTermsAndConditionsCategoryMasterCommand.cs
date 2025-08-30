using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;

public record UpdateTermsAndConditionsCategoryMasterCommand
    (long id, UpdateTermsAndConditionsCategoryMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateTermsAndConditionsCategoryMasterCommandHandler(ITermsAndConditionsCategoryMasterRepository _repository) : ICommandHandler<UpdateTermsAndConditionsCategoryMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateTermsAndConditionsCategoryMasterCommand request, CancellationToken cancellationToken)
    {
        if (request.id != request.requestDto.Id)
        {
            return ApiResponse.Failure(ApiMessage.EntityIdMismatch);
        }

        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);
        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        entity.UpdateFromDto(request.requestDto);
        var result = await _repository.UpdateAsync(entity, cancellationToken);

        if (result)
        {
            var entityDto = entity.Adapt<TermsAndConditionsCategoryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateTermsAndConditionsCategoryMasterCommandValidator : AbstractValidator<UpdateTermsAndConditionsCategoryMasterCommand>
{
    public UpdateTermsAndConditionsCategoryMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}