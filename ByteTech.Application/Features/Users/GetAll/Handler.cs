using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.IRepositories;
using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Users.GetAll;

public class Handler(IUserElasticsearchService userElasticsearchService) : IRequestHandler<Query, BaseResponse>
{
    private readonly IUserElasticsearchService _userElasticsearchService = userElasticsearchService;
    
    public async Task<BaseResponse> Handle(Query request, CancellationToken cancellationToken)
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
