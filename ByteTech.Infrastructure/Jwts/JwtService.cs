using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ByteTech.Application.Services.Jwt;
using ByteTech.Domain.Entities;
using ByteTech.Infrastructure.Configurations;
using Microsoft.IdentityModel.Tokens;

namespace ByteTech.Infrastructure.Jwts;

public class JwtService(JwtSettings settings) : IJwtService
{
    private readonly JwtSettings _jwtSettings = settings;

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
            new (ClaimTypes.Email, user.Email),
            new (ClaimTypes.GivenName, user.FullName),
            new (ClaimTypes.Role, user.Role.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
        return accessToken;
    }
}