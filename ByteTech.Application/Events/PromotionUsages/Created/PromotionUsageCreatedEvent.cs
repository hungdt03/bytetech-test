using ByteTech.Domain.Entities;
using MediatR;

namespace ByteTech.Application.Events.PromotionUsages.Created;

public record PromotionUsageCreatedEvent(PromotionUsage PromotionUsage) : INotification;