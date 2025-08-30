using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.BankMaster;
using DigitalHub.Domain.Interfaces.Master;
using FluentValidation;
using Mapster;
using BankMasterEntity = DigitalHub.Domain.Entities.Master.BankMaster;

namespace DigitalHub.Application.Services.Master.BankMaster;

public record CreateBankMasterCommand
    (CreateBankMasterDto requestDto, CancellationToken cancellationToken = default) : ICommand<ApiResponse>;


public class CreateBankMasterCommandHandler(IBankMasterRepository _repository) : ICommandHandler<CreateBankMasterCommand, ApiResponse>
{
    public async Task<ApiResponse> Handle(CreateBankMasterCommand request, CancellationToken cancellationToken = default)
    {
        var entity = request.requestDto.Adapt<BankMasterEntity>();

        var result = await _repository.AddAsync(entity, cancellationToken);
        if (result)
        {
            var entityDto = entity.Adapt<BankMasterDto>();
            return ApiResponse.Success(ApiMessage.SuccessfulCreate, entityDto);
        }
        return ApiResponse.Failure(ApiMessage.FailedCreate);
    }
}



public sealed class CreateBankMasterCommandValidator : AbstractValidator<CreateBankMasterCommand>
{
    public CreateBankMasterCommandValidator()
    {
        RuleFor(x => x.requestDto.BankName)
             .NotEmpty()
             .MinimumLength(2)
             .MaximumLength(50);
    }
}
