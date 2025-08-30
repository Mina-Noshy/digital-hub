using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.FloorMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using FloorMasterEntity = DigitalHub.Domain.Entities.Master.FloorMaster;

namespace DigitalHub.Application.Services.Master.FloorMaster;

public record CreateFloorMasterCommand
    (CreateFloorMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateFloorMasterCommandHandler(IFloorMasterRepository _repository) : ICommandHandler<CreateFloorMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateFloorMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<FloorMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<FloorMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateFloorMasterCommandValidator : AbstractValidator<CreateFloorMasterCommand>
{
    public CreateFloorMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
