using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.DepartmentMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;
using DepartmentMasterEntity = DigitalHub.Domain.Entities.HR.DepartmentMaster;

namespace DigitalHub.Application.Services.Master.DepartmentMaster;

public record CreateDepartmentMasterCommand
    (CreateDepartmentMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateDepartmentMasterCommandHandler(IDepartmentMasterRepository _repository) : ICommandHandler<CreateDepartmentMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateDepartmentMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<DepartmentMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<DepartmentMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateDepartmentMasterCommandValidator : AbstractValidator<CreateDepartmentMasterCommand>
{
    public CreateDepartmentMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
