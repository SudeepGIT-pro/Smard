using Microsoft.EntityFrameworkCore;
using Smard.Models;
using System.Security.Cryptography;
using System.Text;

namespace Smard.Data
{
    public static class DbSeeder
    {
        /// <summary>
        /// Applies any pending EF Core migrations and seeds the default user
        /// whose credentials are read from the "SeedUser" section in appsettings.json.
        /// </summary>
        public static async Task SeedDatabaseAsync(SmardDbContext context, IConfiguration configuration)
        {
            // Apply pending migrations (creates the DB if it doesn't exist yet).
            // This replaces EnsureCreated() so that the Migrations history table is respected.
            await context.Database.MigrateAsync();

            // Read seed credentials from configuration instead of hardcoding them.
            var seedSection = configuration.GetSection("SeedUser");
            var seedName     = seedSection["Name"]     ?? "Admin";
            var seedEmail    = seedSection["Email"]    ?? throw new InvalidOperationException("SeedUser:Email is not configured.");
            var seedPassword = seedSection["Password"] ?? throw new InvalidOperationException("SeedUser:Password is not configured.");

            // Only seed if the user doesn't already exist.
            if (!await context.Users.AnyAsync(u => u.Email == seedEmail))
            {
                context.Users.Add(new User
                {
                    Name         = seedName,
                    Email        = seedEmail,
                    PasswordHash = HashPassword(seedPassword),
                    Role         = "Admin"
                });

                await context.SaveChangesAsync();
            }

            // Ensure default roles exist
            if (!await context.AspNetRoles.AnyAsync())
            {
                context.AspNetRoles.AddRange(
                    new AspNetRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                    new AspNetRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
                    new AspNetRole { Id = "3", Name = "User", NormalizedName = "USER" }
                );

                await context.SaveChangesAsync();
            }

            // Ensure default user roles exist
            if (!await context.AspNetUserRoles.AnyAsync())
            {
                context.AspNetUserRoles.AddRange(
                    new AspNetUserRole { UserId = "61336a0f-403b-4371-831e-8a843571f4db", RoleId = "1" },
                    new AspNetUserRole { UserId = "053a9fc0-9f36-4d46-8b1d-a5fec686348e", RoleId = "2" }
                );

                await context.SaveChangesAsync();
            }

            // Ensure sudeep@smard.com is always fixed with the Admin role
            var sudeepUser = await context.Users.FirstOrDefaultAsync(u => u.Email == seedEmail);
            if (sudeepUser != null)
            {
                if (sudeepUser.Role != "Admin")
                {
                    sudeepUser.Role = "Admin";
                    await context.SaveChangesAsync();
                }

                if (!await context.AspNetUserRoles.AnyAsync(ur => ur.UserId == sudeepUser.Id.ToString() && ur.RoleId == "1"))
                {
                    context.AspNetUserRoles.Add(new AspNetUserRole
                    {
                        UserId = sudeepUser.Id.ToString(),
                        RoleId = "1"
                    });
                    await context.SaveChangesAsync();
                }
            }

            // Seed initial sample notifications if none exist
            if (!await context.UserNotifications.AnyAsync())
            {
                var targetId = sudeepUser?.Id.ToString() ?? "1";
                var now = DateTime.Now;

                context.UserNotifications.AddRange(
                    new UserNotification
                    {
                        UserId = targetId,
                        Title = "Wall Finishes Added",
                        Message = "'Strip 2' was added to Wall Finishes by Sudeep.",
                        NotifType = "Create",
                        IsRead = false,
                        CreatedOn = now.AddMinutes(-12),
                        ActionUrl = "/WallPanelSystem"
                    },
                    new UserNotification
                    {
                        UserId = targetId,
                        Title = "Wall Finishes Updated",
                        Message = "'Wall Finishes' in Wall Finishes was updated by Sudeep.",
                        NotifType = "Update",
                        IsRead = false,
                        CreatedOn = now.AddMinutes(-35),
                        ActionUrl = "/WallPanelSystem"
                    },
                    new UserNotification
                    {
                        UserId = targetId,
                        Title = "Doors Added",
                        Message = "'Main Double Door' was added to Doors by Sudeep.",
                        NotifType = "Create",
                        IsRead = false,
                        CreatedOn = now.AddHours(-2),
                        ActionUrl = "/Doors"
                    }
                );

                await context.SaveChangesAsync();
            }

            // Seed sample DeleteRequest matching Image 3 if none exist
            if (!await context.DeleteRequests.AnyAsync())
            {
                context.DeleteRequests.Add(new DeleteRequest
                {
                    RequestedByUserId = "2",
                    RequestedByUserName = "Sisira K K",
                    MasterName = "Decking System",
                    ControllerName = "WallPanelSystem",
                    RecordId = 0,
                    TypeIndex = 3,
                    TypeName = "DECKING WITH CEMENT BOARD ONLY",
                    Status = "Pending",
                    RequestedOn = new DateTime(2026, 9, 23, 11, 22, 0),
                    ActionUrl = "/WallPanelSystem"
                });

                await context.SaveChangesAsync();
            }
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
}
