using ByteTech.Application.Common;
using ByteTech.Domain.Enums;
using MediatR;

namespace ByteTech.Application.Features.Promotions.Create;

public record CreatePromotionCommand(string Name,
        string Code,
        int LimitPerUser,
        bool IsLimited,
        string? Description,
        EDiscountType DiscountType,
        decimal DiscountValue,
        DateTime StartDate,
        DateTime EndDate,
        bool IsActive
) : IRequest<BaseResponse>;
