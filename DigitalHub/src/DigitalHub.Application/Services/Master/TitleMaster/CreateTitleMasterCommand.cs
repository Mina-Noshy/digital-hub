using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.TitleMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using TitleMasterEntity = DigitalHub.Domain.Entities.Master.TitleMaster;

namespace DigitalHub.Application.Services.Master.TitleMaster;

public record CreateTitleMasterCommand
    (CreateTitleMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateTitleMasterCommandHandler(ITitleMasterRepository _repository) : ICommandHandler<CreateTitleMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateTitleMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<TitleMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<TitleMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateTitleMasterCommandValidator : AbstractValidator<CreateTitleMasterCommand>
{
    public CreateTitleMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
