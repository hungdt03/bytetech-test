using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetUseds;

public class GetUsedPromotionsHandler(IPromotionUsageElasticsearchService promotionUsageElasticsearchService)
    : IRequestHandler<GetUsedPromotionsQuery, BaseResponse>
{
    private readonly IPromotionUsageElasticsearchService _promotionUsageElasticsearchService = promotionUsageElasticsearchService;

    public async Task<BaseResponse> Handle(GetUsedPromotionsQuery request, CancellationToken cancellationToken)
    {
        var usages = await _promotionUsageElasticsearchService.GetByUserIdAsync(request.UserId);

        return new DataResponse<List<PromotionUsageResponse>>
        {
            Success = true,
            StatusCode = HttpStatusCode.OK,
            Message = "Lấy danh sách khuyến mãi người dùng đã sử dụng thành công.",
            Data = [..usages]
        };
    }
}
