using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CountryMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using CountryMasterEntity = DigitalHub.Domain.Entities.Master.CountryMaster;

namespace DigitalHub.Application.Services.Master.CountryMaster;

public record CreateCountryMasterCommand
    (CreateCountryMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateCountryMasterCommandHandler(ICountryMasterRepository _repository) : ICommandHandler<CreateCountryMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateCountryMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<CountryMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<CountryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateCountryMasterCommandValidator : AbstractValidator<CreateCountryMasterCommand>
{
    public CreateCountryMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
