using ByteTech.Application.Contracts.Responses;
using ByteTech.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ByteTech.Application.Mappers;

[Mapper]
public partial class UserMapper
{
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    [MapperIgnoreSource(nameof(User.CreatedAt))]
    [MapperIgnoreSource(nameof(User.UpdatedAt))]
    public partial UserResponse ToResponse(User user);
}