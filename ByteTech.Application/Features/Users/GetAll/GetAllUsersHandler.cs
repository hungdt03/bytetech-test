using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Users.GetAll;

public class GetAllUsersHandler(IUserElasticsearchService userElasticsearchService) : IRequestHandler<GetAllUsersQuery, BaseResponse>
{
    private readonly IUserElasticsearchService _userElasticsearchService = userElasticsearchService;
    
    public async Task<BaseResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userElasticsearchService.SearchAsync(request.SearchText);
        return new DataResponse<List<UserResponse>>
        {
            Data = [.. users],
            Message = "Lấy danh sách người dùng thành công",
            StatusCode = HttpStatusCode.OK,
            Success = true
        };
    }
}
