using ByteTech.Domain.Enums;

namespace ByteTech.Domain.Entities;

public class PromotionUsage 
{
    public string UserId { get; set; } = default!;
    public string PromotionId { get; set; } = default!;
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    public decimal DiscountAmount { get; set; }
    public EDiscountType DiscountType { get; set; }
}
