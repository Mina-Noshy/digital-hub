using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmployeeMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmployeeMaster;

public record UpdateEmployeeMasterCommand
    (long id, UpdateEmployeeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateEmployeeMasterCommandHandler(IEmployeeMasterRepository _repository) : ICommandHandler<UpdateEmployeeMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateEmployeeMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<EmployeeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateEmployeeMasterCommandValidator : AbstractValidator<UpdateEmployeeMasterCommand>
{
    public UpdateEmployeeMasterCommandValidator()
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