using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetAll;

public class GetAllPromotionsHandler(IPromotionElasticsearchService promotionElasticsearchService)
    : IRequestHandler<GetAllPromotionsQuery, BaseResponse>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;

    public async Task<BaseResponse> Handle(GetAllPromotionsQuery request, CancellationToken cancellationToken)
    {
        var results = await _promotionElasticsearchService.SearchAsync(request.SearchText);

        return new DataResponse<List<PromotionResponse>>()
        {
            Success = true,
            StatusCode = HttpStatusCode.OK,
            Message = string.IsNullOrWhiteSpace(request.SearchText)
                ? "Lấy danh sách khuyến mãi thành công"
                : "Tìm kiếm khuyến mãi thành công",
            Data = [..results]
        };
    }
}
