using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Events.Promotions.Created;

public record PromotionCreatedEvent(Promotion Promotion) : INotification;
