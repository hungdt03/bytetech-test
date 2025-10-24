using ByteTech.Application.IRepositories;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Deleted;

public class CleanupPromotionUsageHandler(IPromotionUsageRepository promotionUsageRepository) : INotificationHandler<PromotionDeletedEvent>
{
    private readonly IPromotionUsageRepository _promotionUsageRepository = promotionUsageRepository;

    public async Task Handle(PromotionDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _promotionUsageRepository.DeleteByPromotionIdAsync(notification.PromotionId);
    }
}