using ByteTech.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteTech.Domain.Entities;

public class PromotionUsage : BaseEntity
{
    public string UserId { get; set; } = default!;
    public string PromotionId { get; set; } = default!;
    public DateTime UsedAt { get; set; } = DateTime.UtcNow;
    public decimal DiscountAmount { get; set; }

    [BsonRepresentation(BsonType.String)]
    public EDiscountType DiscountType { get; set; }
}
