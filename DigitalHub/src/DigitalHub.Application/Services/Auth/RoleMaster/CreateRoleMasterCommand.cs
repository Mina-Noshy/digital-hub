using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RoleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using RoleMasterEntity = DigitalHub.Domain.Entities.Auth.Identity.RoleMaster;
using RolePageMasterEntity = DigitalHub.Domain.Entities.Auth.RolePageMaster;

namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record CreateRoleMasterCommand
    (CreateRoleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateRoleMasterCommandHandler(IRoleMasterRepository _repository, IRolePageMasterRepository _rolePageRepository) : ICommandHandler<CreateRoleMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateRoleMasterCommand request, CancellationToken cancellationToken = default)
    {
        request.requestDto.ConcurrencyStamp = Guid.NewGuid().ToString();

        var entity = request.requestDto.Adapt<RoleMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            await AssignPagesToRole(request.requestDto, cancellationToken);

            var entityDto = entity.Adapt<RoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }


    private async Task AssignPagesToRole(CreateRoleMasterDto request, CancellationToken cancellationToken = default)
    {
        if (request.Pages == null || !request.Pages.Any())
            return;

        // Retrieve the existing role
        var roleEntity = await _repository.GetRoleByNameAsync(request.Name, cancellationToken);
        if (roleEntity == null)
            return;

        var rolePages = request.Pages.Select(x => new RolePageMasterEntity()
        {
            RoleId = roleEntity.Id,
            PageId = x.PageId,
            Create = x.Create,
            Update = x.Update,
            Delete = x.Delete,
            Print = x.Print,
            Export = x.Export,
        });

        await _rolePageRepository.AddRangeAsync(rolePages, cancellationToken);
    }

}



public sealed class CreateRoleMasterCommandValidator : AbstractValidator<CreateRoleMasterCommand>
{
    public CreateRoleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(1)
             .MaximumLength(50);

        RuleFor(x => x.requestDto.NormalizedName)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(50);
    }
}
