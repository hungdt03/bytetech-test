using ByteTech.Application.IRepositories;
using ByteTech.Application.Mappers;
using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.PromotionUsages.Created;

public class AddPromotionUsageToSearchIndexHandler(IPromotionUsageElasticsearchService promotionUsageElasticsearchService, IUserRepository userRepository) : INotificationHandler<PromotionUsageCreatedEvent>
{
    private readonly IPromotionUsageElasticsearchService _promotionUsageElasticsearchService = promotionUsageElasticsearchService;
    private readonly PromotionUsageMapper Mapper = new(userRepository);

    public async Task Handle(PromotionUsageCreatedEvent notification, CancellationToken cancellationToken)
    {
        var payload = Mapper.ToResponse(notification.PromotionUsage);
        await _promotionUsageElasticsearchService.IndexDocumentAsync(payload);
    }
}
