using System.Net;
using ByteTech.Application.Common;
using ByteTech.Application.Contracts.Responses;
using ByteTech.Application.Exceptions;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Features.Promotions.GetById;

public class GetPromotionByIdHandler(IPromotionElasticsearchService promotionElasticsearchService)
    : IRequestHandler<GetPromotionByIdQuery, BaseResponse>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;

    public async Task<BaseResponse> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
    {
        var promotion = await _promotionElasticsearchService.GetDocumentAsync(request.Id) ?? throw new NotFoundException("Không tìm thấy thông tin khuyến mãi");
        
        return new DataResponse<PromotionResponse>()
        {
            Success = true,
            StatusCode = HttpStatusCode.OK,
            Message = "Lấy thông tin khuyến mãi thành công",
            Data = promotion
        };
    }
}