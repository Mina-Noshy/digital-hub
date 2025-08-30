using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.CommunicationMethodMaster;

public record UpdateCommunicationMethodMasterCommand
    (long id, UpdateCommunicationMethodMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateCommunicationMethodMasterCommandHandler(ICommunicationMethodMasterRepository _repository) : ICommandHandler<UpdateCommunicationMethodMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateCommunicationMethodMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<CommunicationMethodMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateCommunicationMethodMasterCommandValidator : AbstractValidator<UpdateCommunicationMethodMasterCommand>
{
    public UpdateCommunicationMethodMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}