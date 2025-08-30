using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;
using EmploymentTypeMasterEntity = DigitalHub.Domain.Entities.HR.EmploymentTypeMaster;

namespace DigitalHub.Application.Services.Master.EmploymentTypeMaster;

public record CreateEmploymentTypeMasterCommand
    (CreateEmploymentTypeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateEmploymentTypeMasterCommandHandler(IEmploymentTypeMasterRepository _repository) : ICommandHandler<CreateEmploymentTypeMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateEmploymentTypeMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<EmploymentTypeMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<EmploymentTypeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateEmploymentTypeMasterCommandValidator : AbstractValidator<CreateEmploymentTypeMasterCommand>
{
    public CreateEmploymentTypeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
