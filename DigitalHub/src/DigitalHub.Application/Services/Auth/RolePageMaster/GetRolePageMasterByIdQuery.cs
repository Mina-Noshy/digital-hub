using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.RolePageMaster;
using DigitalHub.Domain.Interfaces.Auth;

namespace DigitalHub.Application.Services.Auth.RolePageMaster;

public record GetRolePageMasterByIdQuery
    (long id, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;

public class GetRolePageMasterByIdQueryHandler(IRolePageMasterRepository _repository) : IQueryHandler<GetRolePageMasterByIdQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetRolePageMasterByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (entity == null)
        {
            return ApiResponse.Failure(ApiMessage.ItemNotFound);
        }

        var entityDto = new RolePageMasterDto()
        {
            Id = entity.Id,
            RoleId = entity.RoleId,
            PageId = entity.PageId,
            Role = entity.GetRole.Name,
            Page = entity.GetPage.Name,
            Menu = entity.GetPage.GetMenu.Name,
            Module = entity.GetPage.GetMenu.GetModule!.Name,
            Create = entity.Create,
            Delete = entity.Delete,
            Export = entity.Export,
            Print = entity.Print,
            Update = entity.Update
        };

        return ApiResponse.Success(data: entityDto);
    }
}
