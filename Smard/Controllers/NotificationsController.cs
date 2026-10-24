using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smard.Services;
using System.Security.Claims;

namespace Smard.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("GetRecent")]
        public async Task<IActionResult> GetRecent()
        {
            var userId = GetCurrentUserId();
            var userRole = GetCurrentUserRole();

            var unreadCount = await _notificationService.GetUnreadCountAsync(userId, userRole);
            var notifications = await _notificationService.GetRecentNotificationsAsync(userId, userRole);

            return Json(new
            {
                success = true,
                unreadCount,
                notifications
            });
        }

        [HttpPost("MarkAllAsRead")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetCurrentUserId();
            var userRole = GetCurrentUserRole();

            await _notificationService.MarkAllAsReadAsync(userId, userRole);
            return Json(new { success = true });
        }

        [HttpPost("MarkAsRead")]
        public async Task<IActionResult> MarkAsRead([FromQuery] int id)
        {
            var userId = GetCurrentUserId();
            var userRole = GetCurrentUserRole();

            var success = await _notificationService.MarkAsReadAsync(id, userId, userRole);
            return Json(new { success });
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var userId = GetCurrentUserId();
            var userRole = GetCurrentUserRole();

            var success = await _notificationService.DeleteNotificationAsync(id, userId, userRole);
            return Json(new { success });
        }

        private string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? "1";
        }

        private string? GetCurrentUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}
