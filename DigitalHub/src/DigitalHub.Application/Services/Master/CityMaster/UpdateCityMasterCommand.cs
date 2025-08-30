using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CityMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record UpdateCityMasterCommand
    (long id, UpdateCityMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;



public class UpdateCityMasterCommandHandler(ICityMasterRepository _repository) : ICommandHandler<UpdateCityMasterCommand, ApiResponse>
{

    public async Task<ApiResponse> Handle(UpdateCityMasterCommand request, CancellationToken cancellationToken)
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
            var entityDto = entity.Adapt<CityMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulUpdate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedUpdate);
    }
}


public sealed class UpdateCityMasterCommandValidator : AbstractValidator<UpdateCityMasterCommand>
{
    public UpdateCityMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}