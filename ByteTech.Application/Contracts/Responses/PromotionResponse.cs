using ByteTech.Domain.Enums;

namespace ByteTech.Application.Contracts.Responses;

public record PromotionResponse(string Id, string Name, string Code, string Description, int LimitPerUser, decimal DiscountValue, bool IsActive, EDiscountType DiscountType, bool IsLimited, DateTime StartDate, DateTime EndDate);
