using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyTypeMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.PropertyTypeMaster;

public record UpdatePropertyTypeMasterCommand
    (long id, UpdatePropertyTypeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdatePropertyTypeMasterCommandHandler(IPropertyTypeMasterRepository _repository) : ICommandHandler<UpdatePropertyTypeMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdatePropertyTypeMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<PropertyTypeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdatePropertyTypeMasterCommandValidator : AbstractValidator<UpdatePropertyTypeMasterCommand>
{
    public UpdatePropertyTypeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}