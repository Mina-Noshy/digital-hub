using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.UnitCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using UnitCategoryMasterEntity = DigitalHub.Domain.Entities.Master.UnitCategoryMaster;

namespace DigitalHub.Application.Services.Master.UnitCategoryMaster;

public record CreateUnitCategoryMasterCommand
    (CreateUnitCategoryMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateUnitCategoryMasterCommandHandler(IUnitCategoryMasterRepository _repository) : ICommandHandler<CreateUnitCategoryMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateUnitCategoryMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<UnitCategoryMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<UnitCategoryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateUnitCategoryMasterCommandValidator : AbstractValidator<CreateUnitCategoryMasterCommand>
{
    public CreateUnitCategoryMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
