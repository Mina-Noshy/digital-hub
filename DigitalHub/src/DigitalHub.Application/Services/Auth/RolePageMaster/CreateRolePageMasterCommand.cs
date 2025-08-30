using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using RolePageMasterEntity = DigitalHub.Domain.Entities.Auth.RolePageMaster;

namespace DigitalHub.Application.Services.Auth.RolePageMaster;

public record CreateRolePageMasterCommand
    (CreateRolePageMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateRolePageMasterCommandHandler(IRolePageMasterRepository _repository) : ICommandHandler<CreateRolePageMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateRolePageMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<RolePageMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<RolePageMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateRolePageMasterCommandValidator : AbstractValidator<CreateRolePageMasterCommand>
{
    public CreateRolePageMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.RoleId)
             .NotNull();

        RuleFor(x => x.requestDto.PageId)
            .NotNull();
    }
}
