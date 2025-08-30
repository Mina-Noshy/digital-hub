using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.RolePageMaster;

public record UpdateRolePageMasterCommand
    (long id, UpdateRolePageMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateRolePageMasterCommandHandler(IRolePageMasterRepository _repository) : ICommandHandler<UpdateRolePageMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateRolePageMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<RolePageMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateRolePageMasterCommandValidator : AbstractValidator<UpdateRolePageMasterCommand>
{
    public UpdateRolePageMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.RoleId)
             .NotNull();

        RuleFor(x => x.requestDto.PageId)
            .NotNull();
    }
}