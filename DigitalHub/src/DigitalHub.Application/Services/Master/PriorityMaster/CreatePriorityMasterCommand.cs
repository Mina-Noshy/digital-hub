using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PriorityMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using PriorityMasterEntity = DigitalHub.Domain.Entities.Master.PriorityMaster;

namespace DigitalHub.Application.Services.Master.PriorityMaster;

public record CreatePriorityMasterCommand
    (CreatePriorityMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreatePriorityMasterCommandHandler(IPriorityMasterRepository _repository) : ICommandHandler<CreatePriorityMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreatePriorityMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<PriorityMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<PriorityMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreatePriorityMasterCommandValidator : AbstractValidator<CreatePriorityMasterCommand>
{
    public CreatePriorityMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
