using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Users.GetById;

public class GetUserByIdHandler(IUserElasticsearchService userElasticsearchService) : IRequestHandler<GetUserByIdQuery, BaseResponse>
{
    private readonly IUserElasticsearchService _userElasticsearchService = userElasticsearchService;

    public async Task<BaseResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userElasticsearchService.GetDocumentAsync(request.Id);

        return new DataResponse<UserResponse>
        {
            Data = user,
            Message = "Lấy thông tin người dùng thành công",
            StatusCode = System.Net.HttpStatusCode.OK,
            Success = true
        };
    }
}
