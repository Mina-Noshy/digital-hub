using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.PropertyCategoryMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using PropertyCategoryMasterEntity = DigitalHub.Domain.Entities.Master.PropertyCategoryMaster;

namespace DigitalHub.Application.Services.Master.PropertyCategoryMaster;

public record CreatePropertyCategoryMasterCommand
    (CreatePropertyCategoryMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreatePropertyCategoryMasterCommandHandler(IPropertyCategoryMasterRepository _repository) : ICommandHandler<CreatePropertyCategoryMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreatePropertyCategoryMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<PropertyCategoryMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<PropertyCategoryMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreatePropertyCategoryMasterCommandValidator : AbstractValidator<CreatePropertyCategoryMasterCommand>
{
    public CreatePropertyCategoryMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
