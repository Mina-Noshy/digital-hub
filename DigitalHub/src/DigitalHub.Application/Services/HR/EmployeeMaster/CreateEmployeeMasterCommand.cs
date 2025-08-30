using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;
using EmployeeMasterEntity = DigitalHub.Domain.Entities.HR.EmployeeMaster;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record CreateEmployeeMasterCommand
    (CreateEmployeeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateEmployeeMasterCommandHandler(IEmployeeMasterRepository _repository) : ICommandHandler<CreateEmployeeMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateEmployeeMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<EmployeeMasterEntity>();
        entity.EmployeeCode = await _repository.GetNextEmployeeCodeAsync(cancellationToken);

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<EmployeeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateEmployeeMasterCommandValidator : AbstractValidator<CreateEmployeeMasterCommand>
{
    public CreateEmployeeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.FirstName)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);

        RuleFor(x => x.requestDto.LastName)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
