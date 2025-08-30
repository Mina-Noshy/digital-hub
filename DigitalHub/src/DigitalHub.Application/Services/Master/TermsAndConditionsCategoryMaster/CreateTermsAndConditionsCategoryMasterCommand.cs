using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using TermsAndConditionsCategoryMasterEntity = DigitalHub.Domain.Entities.Master.TermsAndConditionsCategoryMaster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsCategoryMaster;

public record CreateTermsAndConditionsCategoryMasterCommand
    (CreateTermsAndConditionsCategoryMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateTermsAndConditionsCategoryMasterCommandHandler(ITermsAndConditionsCategoryMasterRepository _repository) : ICommandHandler<CreateTermsAndConditionsCategoryMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateTermsAndConditionsCategoryMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<TermsAndConditionsCategoryMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<TermsAndConditionsCategoryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateTermsAndConditionsCategoryMasterCommandValidator : AbstractValidator<CreateTermsAndConditionsCategoryMasterCommand>
{
    public CreateTermsAndConditionsCategoryMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
