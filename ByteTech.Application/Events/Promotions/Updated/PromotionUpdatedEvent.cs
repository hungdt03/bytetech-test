using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Updated;

public record PromotionUpdatedEvent(Promotion Promotion) : INotification;

