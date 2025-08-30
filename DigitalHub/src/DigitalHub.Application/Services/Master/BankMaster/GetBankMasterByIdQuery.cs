using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Master.BankMaster;
using DigitalHub.Domain.Interfaces.Master;
using Mapster;

namespace DigitalHub.Application.Services.Master.BankMaster;

public record GetBankMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetBankMasterByIdQueryHandler(IBankMasterRepository _repository) : IQueryHandler<GetBankMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetBankMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = entity.Adapt<BankMasterDto>();

        return ApiResponse.Success(data: entityDto);
    }
}
