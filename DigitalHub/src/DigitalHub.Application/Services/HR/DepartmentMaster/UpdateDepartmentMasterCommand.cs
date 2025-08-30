using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.DepartmentMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record UpdateDepartmentMasterCommand
    (long id, UpdateDepartmentMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateDepartmentMasterCommandHandler(IDepartmentMasterRepository _repository) : ICommandHandler<UpdateDepartmentMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateDepartmentMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<DepartmentMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateDepartmentMasterCommandValidator : AbstractValidator<UpdateDepartmentMasterCommand>
{
    public UpdateDepartmentMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}