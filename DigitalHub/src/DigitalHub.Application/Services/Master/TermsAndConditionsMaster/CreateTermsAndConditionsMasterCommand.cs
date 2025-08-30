using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using TermsAndConditionsMasterEntity = DigitalHub.Domain.Entities.Master.TermsAndConditionsMaster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsMaster;

public record CreateTermsAndConditionsMasterCommand
    (CreateTermsAndConditionsMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateTermsAndConditionsMasterCommandHandler(ITermsAndConditionsMasterRepository _repository) : ICommandHandler<CreateTermsAndConditionsMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateTermsAndConditionsMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<TermsAndConditionsMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<TermsAndConditionsMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateTermsAndConditionsMasterCommandValidator : AbstractValidator<CreateTermsAndConditionsMasterCommand>
{
    public CreateTermsAndConditionsMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Description)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(300);
    }
}
