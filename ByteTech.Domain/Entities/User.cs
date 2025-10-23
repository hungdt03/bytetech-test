using ByteTech.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace ByteTech.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public EUserRole Role { get; set; } = EUserRole.Customer;
    public bool IsLocked { get; set; } = false;
}
