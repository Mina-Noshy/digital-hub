using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CityMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using CityMasterEntity = DigitalHub.Domain.Entities.Master.CityMaster;

namespace DigitalHub.Application.Services.Master.CityMaster;

public record CreateCityMasterCommand
    (CreateCityMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateCityMasterCommandHandler(ICityMasterRepository _repository) : ICommandHandler<CreateCityMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateCityMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<CityMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<CityMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateCityMasterCommandValidator : AbstractValidator<CreateCityMasterCommand>
{
    public CreateCityMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
