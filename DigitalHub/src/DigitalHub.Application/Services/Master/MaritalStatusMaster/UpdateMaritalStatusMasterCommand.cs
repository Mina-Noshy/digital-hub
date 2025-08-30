using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.MaritalStatusMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.MaritalStatusMaster;

public record UpdateMaritalStatusMasterCommand
    (long id, UpdateMaritalStatusMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateMaritalStatusMasterCommandHandler(IMaritalStatusMasterRepository _repository) : ICommandHandler<UpdateMaritalStatusMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateMaritalStatusMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<MaritalStatusMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateMaritalStatusMasterCommandValidator : AbstractValidator<UpdateMaritalStatusMasterCommand>
{
    public UpdateMaritalStatusMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}