using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record UpdateEmployeeRoleMasterCommand
    (long id, UpdateEmployeeRoleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateEmployeeRoleMasterCommandHandler(IEmployeeRoleMasterRepository _repository) : ICommandHandler<UpdateEmployeeRoleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateEmployeeRoleMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<EmployeeRoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateEmployeeRoleMasterCommandValidator : AbstractValidator<UpdateEmployeeRoleMasterCommand>
{
    public UpdateEmployeeRoleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}