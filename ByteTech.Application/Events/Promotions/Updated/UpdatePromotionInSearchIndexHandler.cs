using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Updated;

public class UpdatePromotionInSearchIndexHandler(IPromotionElasticsearchService promotionElasticsearchService) : INotificationHandler<PromotionUpdatedEvent>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;
    private readonly PromotionMapper Mapper = new();

    public async Task Handle(PromotionUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var payload = Mapper.ToResponse(notification.Promotion);
        await _promotionElasticsearchService.UpdateDocumentAsync(payload);
    }
}
