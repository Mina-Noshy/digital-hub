using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RoleMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using RolePageMasterEntity = DigitalHub.Domain.Entities.Auth.RolePageMaster;


namespace DigitalHub.Application.Services.Auth.RoleMaster;

public record UpdateRoleMasterCommand
    (long id, UpdateRoleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateRoleMasterCommandHandler(IRoleMasterRepository _repository, IRolePageMasterRepository _rolePageRepository) : ICommandHandler<UpdateRoleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateRoleMasterCommand request, CancellationToken cancellationToken)
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

        if (!string.IsNullOrEmpty(request.requestDto.ConcurrencyStamp) && entity.ConcurrencyStamp != request.requestDto.ConcurrencyStamp)
        {
            return ApiResponse.Failure("Concurrency conflict detected. The role has been updated by another user.");
        }

        // Update values
        entity.Name = request.requestDto.Name;
        entity.NormalizedName = request.requestDto.NormalizedName;
        entity.ConcurrencyStamp = Guid.NewGuid().ToString();
        entity.DefaultFor = request.requestDto.DefaultFor;

        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result)
        {
            await AssignPagesToRole(request.id, request.requestDto.Pages, cancellationToken);

            var entityDto = entity.Adapt<RoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }



    private async Task AssignPagesToRole(long roleId, CreateRolePageItemDto[]? pages, CancellationToken cancellationToken = default)
    {
        var pagesToRemove = await _rolePageRepository.GetAllByRoleIdAsync(roleId, cancellationToken);
        if (pagesToRemove.Any())
        {
            await _rolePageRepository.DeleteRangeAsync(pagesToRemove, cancellationToken);
        }

        if (pages == null || !pages.Any())
        {
            return;
        }

        var pagesToInsert = pages?.Select(x => new RolePageMasterEntity()
        {
            RoleId = roleId,
            PageId = x.PageId,
            Create = x.Create,
            Update = x.Update,
            Delete = x.Delete,
            Print = x.Print,
            Export = x.Export,
        });

        if (pagesToInsert != null && pagesToInsert.Any())
        {
            await _rolePageRepository.AddRangeAsync(pagesToInsert, cancellationToken);
        }
    }


}


public sealed class UpdateRoleMasterCommandValidator : AbstractValidator<UpdateRoleMasterCommand>
{
    public UpdateRoleMasterCommandValidator()
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