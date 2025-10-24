using MediatR;

namespace ByteTech.Application.Events.Promotions.Deleted;

public record PromotionDeletedEvent(string PromotionId) : INotification;

