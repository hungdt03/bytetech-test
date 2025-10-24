using ByteTech.Domain.Enums;

namespace ByteTech.Application.Contracts.Requests;

public record EditPromotionRequest(
    string Name,
    string Code,
    int LimitPerUser,
    bool IsLimited,
    string? Description,
    EDiscountType DiscountType,
    decimal DiscountValue,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive
);
