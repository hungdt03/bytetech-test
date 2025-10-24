using ByteTech.Domain.Enums;

namespace ByteTech.Application.Contracts.Responses;

public record PromotionUsageResponse(
    string Id,
    string UserId,
    string FullName,
    string PromotionId,
    DateTime UsedAt,
    decimal DiscountAmount,
    EDiscountType DiscountType
);
