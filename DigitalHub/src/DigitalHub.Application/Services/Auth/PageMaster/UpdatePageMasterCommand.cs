using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.PageMaster;
using DigitalHub.Domain.Interfaces.Auth;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Auth.PageMaster;

public record UpdatePageMasterCommand
    (long id, UpdatePageMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdatePageMasterCommandHandler(IPageMasterRepository _repository) : ICommandHandler<UpdatePageMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdatePageMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<PageMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdatePageMasterCommandValidator : AbstractValidator<UpdatePageMasterCommand>
{
    public UpdatePageMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}