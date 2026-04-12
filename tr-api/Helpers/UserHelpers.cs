using System.Security.Claims;
using tr_core.Entities;
using tr_service.Exceptions;

namespace tr_backend.Helpers
{
    public static class UserHelpers
    {
        public static string GetUserIdFromClaims(ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedException("User is not logged in");
        }
    }
}
