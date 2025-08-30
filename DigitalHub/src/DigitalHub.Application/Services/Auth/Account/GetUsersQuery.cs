using DigitalHub.Application.Abstractions;
using DigitalHub.Application.Common;
using DigitalHub.Application.DTOs.Auth.Account;
using DigitalHub.Domain.Interfaces.Auth;
using DigitalHub.Domain.QueryParams.Common;
using Mapster;

namespace DigitalHub.Application.Services.Auth.Account;

public record GetUsersQuery
    (PaginationQueryParams queryParams, CancellationToken cancellationToken = default) : IQuery<ApiResponse>;


public class GetUsersQueryHandler(IAccountRepository _repository) : IQueryHandler<GetUsersQuery, ApiResponse>
{
    public async Task<ApiResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _repository.GetUsersCountAsync(request.queryParams, cancellationToken);

        var entityList = await _repository.GetUsersPagedAsync(request.queryParams, cancellationToken);

        var entityListDto = entityList.Adapt<IEnumerable<UserMasterDto>>();

        var pagedResponse = new PagedResponse<UserMasterDto>(entityListDto, request.queryParams.PageNumber, request.queryParams.PageSize, totalCount);

        return ApiResponse.Success(data: pagedResponse);
    }
}