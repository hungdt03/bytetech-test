using ByteTech.Domain.Entities;

namespace ByteTech.Application.Services.Jwt;

public interface IJwtService
{
    string GenerateToken(User user);
}