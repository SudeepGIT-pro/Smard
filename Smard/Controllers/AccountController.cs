using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SmardDbContext _context;

        public AccountController(SmardDbContext context)
        {
            _context = context;
        }

        public IActionResult RequestQuotation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            if (!User.IsAdmin())
            {
                return RedirectToAction(nameof(RequestQuotation));
            }
            return View(new RegisterUserViewModel());
        }

        [HttpGet]
        public IActionResult UserRegistration()
        {
            if (!User.IsAdmin())
            {
                return RedirectToAction(nameof(RequestQuotation));
            }
            return RedirectToAction(nameof(RegisterUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!User.IsAdmin())
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var normalizedEmail = model.Email.Trim().ToLowerInvariant();
            var exists = await _context.Users.AnyAsync(u => u.Email.ToLower() == normalizedEmail);
            if (exists)
            {
                ModelState.AddModelError("Email", "A user with this email address already exists.");
                return View(model);
            }

            // Only "Manager" or "User" can be registered from this form ("Admin" is reserved exclusively for Sudeep)
            var roleName = model.UserType?.Trim() == "Manager" ? "Manager" : "User";

            var newUser = new User
            {
                Name = model.FullName.Trim(),
                Email = normalizedEmail,
                PasswordHash = DbSeeder.HashPassword(model.Password),
                Role = roleName
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Link to AspNetUserRoles table
            var roleEntity = await _context.AspNetRoles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (roleEntity != null)
            {
                _context.AspNetUserRoles.Add(new AspNetUserRole
                {
                    UserId = newUser.Id.ToString(),
                    RoleId = roleEntity.Id
                });
                await _context.SaveChangesAsync();
            }

            ViewBag.SuccessMessage = $"User \"{newUser.Name}\" was registered successfully as \"{roleName}\"!";
            ModelState.Clear();
            return View(new RegisterUserViewModel());
        }
    }
}

