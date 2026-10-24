using Microsoft.EntityFrameworkCore;
using Smard.Data;
using Smard.Models;

namespace Smard.Services
{
    public interface INotificationService
    {
        Task CreateMasterNotificationAsync(string masterName, string typeName, string actionType, string performedByUserName, string? actionUrl);
        Task CreateUserNotificationAsync(string targetUserId, string title, string message, string notifType, string? actionUrl);
        Task<int> GetUnreadCountAsync(string userId, string? userRole = null);
        Task<List<UserNotificationDto>> GetRecentNotificationsAsync(string userId, string? userRole = null, int take = 30);
        Task<bool> MarkAsReadAsync(int notificationId, string userId, string? userRole = null);
        Task MarkAllAsReadAsync(string userId, string? userRole = null);
        Task<bool> DeleteNotificationAsync(int notificationId, string userId, string? userRole = null);
    }

    public class UserNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string NotifType { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string CreatedOn { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public string? ActionUrl { get; set; }
    }

    public class NotificationService : INotificationService
    {
        private readonly SmardDbContext _context;

        public NotificationService(SmardDbContext context)
        {
            _context = context;
        }

        public async Task CreateMasterNotificationAsync(string masterName, string typeName, string actionType, string performedByUserName, string? actionUrl)
        {
            try
            {
                // Determine Title and Message based on actionType
                string title;
                string message;

                switch (actionType?.Trim().ToLowerInvariant())
                {
                    case "create":
                    case "add":
                    case "added":
                        title = $"{masterName} Added";
                        message = $"'{typeName}' was added to {masterName} by {performedByUserName}.";
                        actionType = "Create";
                        break;

                    case "update":
                    case "updated":
                        title = $"{masterName} Updated";
                        message = $"'{typeName}' in {masterName} was updated by {performedByUserName}.";
                        actionType = "Update";
                        break;

                    case "delete":
                    case "deleted":
                        title = $"{masterName} Deleted";
                        message = $"'{typeName}' in {masterName} was deleted by {performedByUserName}.";
                        actionType = "Delete";
                        break;

                    case "deleterequest":
                        title = $"{masterName} Delete Requested";
                        message = $"{performedByUserName} requested to delete '{typeName}' in {masterName}.";
                        actionType = "Delete";
                        break;

                    default:
                        title = $"{masterName} Notification";
                        message = $"'{typeName}' was modified in {masterName} by {performedByUserName}.";
                        actionType = "Info";
                        break;
                }

                // Identify all Admin and Manager users who should receive the notification
                var adminAndManagerUserIds = await _context.Users
                    .Where(u => u.Role == "Admin" || u.Role == "Manager")
                    .Select(u => u.Id.ToString())
                    .ToListAsync();

                var roleUserIds = await (from ur in _context.AspNetUserRoles
                                         join r in _context.AspNetRoles on ur.RoleId equals r.Id
                                         where r.Name == "Admin" || r.Name == "Manager"
                                         select ur.UserId).ToListAsync();

                var targetUserIds = adminAndManagerUserIds.Union(roleUserIds).Distinct().ToList();

                // If no targets configured yet, notify all existing users or default ID
                if (!targetUserIds.Any())
                {
                    targetUserIds = await _context.Users.Select(u => u.Id.ToString()).ToListAsync();
                    if (!targetUserIds.Any())
                    {
                        targetUserIds.Add("1");
                    }
                }

                var now = DateTime.Now;
                var notifications = targetUserIds.Select(targetId => new UserNotification
                {
                    UserId = targetId,
                    Title = title,
                    Message = message,
                    NotifType = actionType,
                    IsRead = false,
                    CreatedOn = now,
                    ActionUrl = actionUrl
                }).ToList();

                await _context.UserNotifications.AddRangeAsync(notifications);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Ensure notification dispatch failure does not crash the caller
            }
        }

        public async Task CreateUserNotificationAsync(string targetUserId, string title, string message, string notifType, string? actionUrl)
        {
            try
            {
                var notif = new UserNotification
                {
                    UserId = targetUserId,
                    Title = title,
                    Message = message,
                    NotifType = notifType,
                    IsRead = false,
                    CreatedOn = DateTime.Now,
                    ActionUrl = actionUrl
                };
                await _context.UserNotifications.AddAsync(notif);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Ensure failure does not crash caller
            }
        }

        public async Task<int> GetUnreadCountAsync(string userId, string? userRole = null)
        {
            var query = BuildUserQuery(userId, userRole);
            return await query.CountAsync(n => !n.IsRead);
        }

        public async Task<List<UserNotificationDto>> GetRecentNotificationsAsync(string userId, string? userRole = null, int take = 30)
        {
            var query = BuildUserQuery(userId, userRole);
            var list = await query
                .OrderByDescending(n => n.CreatedOn)
                .Take(take)
                .ToListAsync();

            return list.Select(n => new UserNotificationDto
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                NotifType = n.NotifType,
                IsRead = n.IsRead,
                CreatedOn = n.CreatedOn.ToString("dd-MMM-yyyy hh:mm tt"),
                TimeAgo = GetTimeAgo(n.CreatedOn),
                ActionUrl = n.ActionUrl
            }).ToList();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, string userId, string? userRole = null)
        {
            var item = await _context.UserNotifications.FirstOrDefaultAsync(n => n.Id == notificationId);
            if (item == null) return false;

            if (item.UserId == userId || userRole == "Admin")
            {
                item.IsRead = true;
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task MarkAllAsReadAsync(string userId, string? userRole = null)
        {
            var query = BuildUserQuery(userId, userRole);
            var unreadItems = await query.Where(n => !n.IsRead).ToListAsync();
            if (unreadItems.Any())
            {
                foreach (var item in unreadItems)
                {
                    item.IsRead = true;
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId, string userId, string? userRole = null)
        {
            var item = await _context.UserNotifications.FirstOrDefaultAsync(n => n.Id == notificationId);
            if (item == null) return false;

            if (item.UserId == userId || userRole == "Admin")
            {
                _context.UserNotifications.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        private IQueryable<UserNotification> BuildUserQuery(string userId, string? userRole)
        {
            // If the user has a specific ID or if user is Admin/Manager and might have "1" as default id
            if (userRole == "Admin")
            {
                return _context.UserNotifications.Where(n => n.UserId == userId || n.UserId == "1");
            }

            return _context.UserNotifications.Where(n => n.UserId == userId);
        }

        private static string GetTimeAgo(DateTime dt)
        {
            var span = DateTime.Now - dt;
            if (span.TotalSeconds < 60) return "Just now";
            if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes} mins ago";
            if (span.TotalHours < 24)
            {
                var h = (int)span.TotalHours;
                return h == 1 ? "1 hour ago" : $"{h} hours ago";
            }
            if (span.TotalDays < 2) return "Yesterday";
            if (span.TotalDays < 7) return $"{(int)span.TotalDays} days ago";
            return dt.ToString("dd-MMM-yyyy");
        }
    }
}
