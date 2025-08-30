using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitStyleMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitStyleMasterEntity = DigitalHub.Domain.Entities.Master.UnitStyleMaster;

namespace DigitalHub.Application.Services.Master.UnitStyleMaster;

public record CreateUnitStyleMasterCommand
    (CreateUnitStyleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitStyleMasterCommandHandler(IUnitStyleMasterRepository _repository) : ICommandHandler<CreateUnitStyleMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitStyleMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitStyleMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitStyleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitStyleMasterCommandValidator : AbstractValidator<CreateUnitStyleMasterCommand>
{
    public CreateUnitStyleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
