using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TermsAndConditionsMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.TermsAndConditionsMaster;

public record UpdateTermsAndConditionsMasterCommand
    (long id, UpdateTermsAndConditionsMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateTermsAndConditionsMasterCommandHandler(ITermsAndConditionsMasterRepository _repository) : ICommandHandler<UpdateTermsAndConditionsMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateTermsAndConditionsMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<TermsAndConditionsMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateTermsAndConditionsMasterCommandValidator : AbstractValidator<UpdateTermsAndConditionsMasterCommand>
{
    public UpdateTermsAndConditionsMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Description)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(300);
    }
}