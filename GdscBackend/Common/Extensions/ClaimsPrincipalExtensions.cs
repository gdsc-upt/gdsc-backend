using System.Security.Claims;

namespace GdscBackend.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "sub");

        if (userIdClaim is not null && !string.IsNullOrEmpty(userIdClaim.Value))
        {
            return userIdClaim.Value;
        }

        return null;
    }
}