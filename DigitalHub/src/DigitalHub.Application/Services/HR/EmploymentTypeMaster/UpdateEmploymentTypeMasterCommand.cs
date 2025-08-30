using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.HR.EmploymentTypeMaster;
using DigitalHub.Domain.Interfaces.HR;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.EmploymentTypeMaster;

public record UpdateEmploymentTypeMasterCommand
    (long id, UpdateEmploymentTypeMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateEmploymentTypeMasterCommandHandler(IEmploymentTypeMasterRepository _repository) : ICommandHandler<UpdateEmploymentTypeMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateEmploymentTypeMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<EmploymentTypeMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateEmploymentTypeMasterCommandValidator : AbstractValidator<UpdateEmploymentTypeMasterCommand>
{
    public UpdateEmploymentTypeMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}