using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Created;

public class AddPromotionToSearchIndexHandler(IPromotionElasticsearchService promotionElasticsearchService) : INotificationHandler<PromotionCreatedEvent>
{
    private readonly IPromotionElasticsearchService _promotionElasticsearchService = promotionElasticsearchService;
    private readonly PromotionMapper Mapper = new();

    public async Task Handle(PromotionCreatedEvent notification, CancellationToken cancellationToken)
    {
        var payload = Mapper.ToResponse(notification.Promotion);
        await _promotionElasticsearchService.IndexDocumentAsync(payload);
    }
}
