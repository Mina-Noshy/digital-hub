using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeRoleMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;
using EmployeeRoleMasterEntity = DigitalHub.Domain.Entities.HR.EmployeeRoleMaster;

namespace DigitalHub.Application.Services.Master.EmployeeRoleMaster;

public record CreateEmployeeRoleMasterCommand
    (CreateEmployeeRoleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateEmployeeRoleMasterCommandHandler(IEmployeeRoleMasterRepository _repository) : ICommandHandler<CreateEmployeeRoleMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateEmployeeRoleMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<EmployeeRoleMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<EmployeeRoleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateEmployeeRoleMasterCommandValidator : AbstractValidator<CreateEmployeeRoleMasterCommand>
{
    public CreateEmployeeRoleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
