using ByteTech.Application.Services.Elasticsearch;
using MediatR;

namespace ByteTech.Application.Events.Users.Updated;

public class UpdatedUserInfoInPromotionUsageHandler(IPromotionUsageElasticsearchService promotionUsageElasticsearchService) : INotificationHandler<UserUpdatedEvent>
{
    private readonly IPromotionUsageElasticsearchService _promotionUsageElasticsearchService = promotionUsageElasticsearchService;

    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        await _promotionUsageElasticsearchService.UpdateUserFullNameAsync(notification.User.Id, notification.User.FullName);
    }
}
