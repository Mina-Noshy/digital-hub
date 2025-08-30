using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.ModuleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.ModuleMaster;

public record UpdateModuleMasterCommand
    (long id, UpdateModuleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateModuleMasterCommandHandler(IModuleMasterRepository _repository) : ICommandHandler<UpdateModuleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateModuleMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<ModuleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateModuleMasterCommandValidator : AbstractValidator<UpdateModuleMasterCommand>
{
    public UpdateModuleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}