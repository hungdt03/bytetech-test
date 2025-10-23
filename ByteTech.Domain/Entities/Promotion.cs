using ByteTech.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteTech.Domain.Entities;

public class Promotion : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public int LimitPerUser { get; set; } = 1;
    public bool IsLimited { get; set; } = false;
    public string? Description { get; set; }

    [BsonRepresentation(BsonType.String)]
    public EDiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; } = true;
}