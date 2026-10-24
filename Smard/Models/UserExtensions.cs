using System.Security.Claims;

namespace Smard
{
    public static class UserExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal? user)
        {
            if (user == null || user.Identity?.IsAuthenticated != true)
                return false;

            return user.IsInRole("Admin")
                || user.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin")
                || string.Equals(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value, "sudeep@smard.com", StringComparison.OrdinalIgnoreCase);
        }
    }
}
