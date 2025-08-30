using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;
using RolePageMasterEntity = DigitalHub.Domain.Entities.Auth.RolePageMaster;



namespace DigitalHub.Application.Services.Auth.RolePageMaster;

public record CreateRangeRolePageMasterCommand
    (IEnumerable<CreateRolePageMasterDto> requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateRangeRolePageMasterCommandHandler(IRolePageMasterRepository _repository) : ICommandHandler<CreateRangeRolePageMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateRangeRolePageMasterCommand request, CancellationToken cancellationToken = default)
    {
        if (!request.requestDto.Any())
        {
            return ApiResponse.Failure("The list of pages should not be empty.");
        }

        if (request.requestDto.Select(x => x.RoleId).Distinct().Count() > 1)
        {
            return ApiResponse.Failure("All items must have the same Role ID.");
        }

        if (request.requestDto.Select(x => x.PageId).Distinct().Count() != request.requestDto.Select(x => x.PageId).Count())
        {
            return ApiResponse.Failure("Duplicate Page IDs are not allowed.");
        }


        await _repository.UnitOfWork.BeginTransactionAsync(cancellationToken);

        long roleId = request.requestDto.FirstOrDefault()?.RoleId ?? 0;
        var entityListToDelete = await _repository.GetAllByRoleIdAsync(roleId, cancellationToken);
        if (entityListToDelete != null)
        {
            await _repository.DeleteRangeAsync(entityListToDelete, cancellationToken);
        }


        var entityList = request.requestDto.Adapt<IEnumerable<RolePageMasterEntity>>();

        var result = await _repository.AddRangeAsync(entityList, cancellationToken);
        if (result)
        {
            await _repository.UnitOfWork.CommitTransactionAsync(cancellationToken);

            var entityListDto = entityList.Adapt<IEnumerable<RolePageMasterDto>>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityListDto);
        }

        await _repository.UnitOfWork.RollbackTransactionAsync(cancellationToken);
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateRangeRolePageMasterCommandValidator : AbstractValidator<CreateRangeRolePageMasterCommand>
{
    public CreateRangeRolePageMasterCommandValidator()
    {
        RuleFor(x => x.requestDto)
             .NotNull();
    }
}
