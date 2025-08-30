using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.CommunicationMethodMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using CommunicationMethodMasterEntity = DigitalHub.Domain.Entities.Master.CommunicationMethodMaster;

namespace DigitalHub.Application.Services.Master.CommunicationMethodMaster;

public record CreateCommunicationMethodMasterCommand
    (CreateCommunicationMethodMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateCommunicationMethodMasterCommandHandler(ICommunicationMethodMasterRepository _repository) : ICommandHandler<CreateCommunicationMethodMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateCommunicationMethodMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<CommunicationMethodMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<CommunicationMethodMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateCommunicationMethodMasterCommandValidator : AbstractValidator<CreateCommunicationMethodMasterCommand>
{
    public CreateCommunicationMethodMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
