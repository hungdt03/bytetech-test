using ByteTech.Domain.Enums;

namespace ByteTech.Application.Contracts.Responses;

public record UserResponse
(
    string Id,
    string Email,
    string FullName,
    EUserRole Role,
    bool IsLocked,
    DateTime CreatedAt,
    DateTime UpdatedAt
);