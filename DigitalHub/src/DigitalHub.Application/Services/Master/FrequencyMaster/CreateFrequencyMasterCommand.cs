using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.FrequencyMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using FrequencyMasterEntity = DigitalHub.Domain.Entities.Master.FrequencyMaster;

namespace DigitalHub.Application.Services.Master.FrequencyMaster;

public record CreateFrequencyMasterCommand
    (CreateFrequencyMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateFrequencyMasterCommandHandler(IFrequencyMasterRepository _repository) : ICommandHandler<CreateFrequencyMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateFrequencyMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<FrequencyMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<FrequencyMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateFrequencyMasterCommandValidator : AbstractValidator<CreateFrequencyMasterCommand>
{
    public CreateFrequencyMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
