using System.Security.Claims;
using ByteTech.Domain.Enums;

namespace ByteTech.Application.Identity;

public static class ClaimPrincipalExtension
{
    public static string GetUserId(this ClaimsPrincipal user) => user?.FindFirstValue(ClaimTypes.NameIdentifier)!;

    public static string GetEmail(this ClaimsPrincipal user) => user?.FindFirstValue(ClaimTypes.Email)!;

    public static string GetFullName(this ClaimsPrincipal user) => user?.FindFirstValue(ClaimTypes.GivenName)!;

    public static EUserRole GetRole(this ClaimsPrincipal user)
    {
        var roleValue = user?.FindFirstValue(ClaimTypes.Role);
        if (string.IsNullOrEmpty(roleValue))
            return default;

        return Enum.Parse<EUserRole>(roleValue);
    }
}