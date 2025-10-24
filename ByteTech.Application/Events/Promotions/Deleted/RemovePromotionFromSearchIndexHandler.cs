using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Deleted;

public class RemovePromotionFromSearchIndexHandler(IPromotionElasticsearchService promotionElasticsearchService, IPromotionUsageElasticsearchService promotionUsageElasticsearchService)
    : INotificationHandler<PromotionDeletedEvent>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;
    private readonly IPromotionUsageElasticsearchService _promotionUsageElasticsearchService = promotionUsageElasticsearchService;

    public async Task Handle(PromotionDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _promotionElasticsearchService.DeleteDocumentAsync(notification.PromotionId);
        await _promotionUsageElasticsearchService.DeleteByPromotionIdAsync(notification.PromotionId);
    }
}