using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TitleMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.TitleMaster;

public record UpdateTitleMasterCommand
    (long id, UpdateTitleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateTitleMasterCommandHandler(ITitleMasterRepository _repository) : ICommandHandler<UpdateTitleMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateTitleMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<TitleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateTitleMasterCommandValidator : AbstractValidator<UpdateTitleMasterCommand>
{
    public UpdateTitleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}