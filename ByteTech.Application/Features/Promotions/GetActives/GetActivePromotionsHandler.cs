using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetActives;

public class GetActivePromotionsHandler(IPromotionElasticsearchService promotionElasticsearchService)
    : IRequestHandler<GetActivePromotionsQuery, BaseResponse>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;

    public async Task<BaseResponse> Handle(GetActivePromotionsQuery request, CancellationToken cancellationToken)
    {
        var activePromotions = await _promotionElasticsearchService.GetActiveAsync();

        return new DataResponse<List<PromotionResponse>>
        {
            Success = true,
            StatusCode = HttpStatusCode.OK,
            Message = "Lấy danh sách khuyến mãi đang hoạt động thành công.",
            Data = [..activePromotions]
        };
    }
}