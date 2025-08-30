using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.NationalityMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using NationalityMasterEntity = DigitalHub.Domain.Entities.Master.NationalityMaster;

namespace DigitalHub.Application.Services.Master.NationalityMaster;

public record CreateNationalityMasterCommand
    (CreateNationalityMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateNationalityMasterCommandHandler(INationalityMasterRepository _repository) : ICommandHandler<CreateNationalityMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateNationalityMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<NationalityMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<NationalityMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateNationalityMasterCommandValidator : AbstractValidator<CreateNationalityMasterCommand>
{
    public CreateNationalityMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.Name)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
