using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.MenuMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.MenuMaster;

public record UpdateMenuMasterCommand
    (long id, UpdateMenuMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateMenuMasterCommandHandler(IMenuMasterRepository _repository) : ICommandHandler<UpdateMenuMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateMenuMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<MenuMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateMenuMasterCommandValidator : AbstractValidator<UpdateMenuMasterCommand>
{
    public UpdateMenuMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}