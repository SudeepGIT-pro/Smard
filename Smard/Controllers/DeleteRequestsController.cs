using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smard.Services;
using System.Security.Claims;

namespace Smard.Controllers
{
    [Authorize]
    public class DeleteRequestsController : Controller
    {
        private readonly IDeleteRequestService _deleteRequestService;

        public DeleteRequestsController(IDeleteRequestService deleteRequestService)
        {
            _deleteRequestService = deleteRequestService;
        }

        private bool IsAdminOrManager()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
            var isAdmin = User.IsInRole("Admin") || (userRole == "Admin") || string.Equals(User.FindFirst(ClaimTypes.Email)?.Value, "sudeep@smard.com", StringComparison.OrdinalIgnoreCase);
            var isManager = User.IsInRole("Manager") || (userRole == "Manager");
            return isAdmin || isManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetPending()
        {
            if (!IsAdminOrManager())
            {
                return Forbid();
            }

            var count = await _deleteRequestService.GetPendingCountAsync();
            var requests = await _deleteRequestService.GetPendingRequestsAsync();
            return Json(new { count, requests });
        }

        [HttpGet]
        public async Task<IActionResult> GetPendingForMaster(string controller)
        {
            var recordIds = await _deleteRequestService.GetPendingRecordIdsAsync(controller);
            return Json(new { recordIds });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRequests()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var requests = await _deleteRequestService.GetUserRequestsAsync(userId);
            return Json(new { requests });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDeleteRequestInput model)
        {
            if (model == null)
            {
                return BadRequest(new { success = false, message = "Invalid delete request payload." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var userName = User.Identity?.Name ?? "User";

            var (success, message, id) = await _deleteRequestService.CreateRequestAsync(model, userId, userName);
            return Json(new { success, message, id });
        }

        public class ApproveRequestModel
        {
            public int Id { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Approve([FromBody] ApproveRequestModel model)
        {
            if (!IsAdminOrManager())
            {
                return Forbid();
            }

            if (model == null || model.Id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid request ID." });
            }

            var reviewerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var reviewerName = User.Identity?.Name ?? "Admin";

            var (success, message) = await _deleteRequestService.ApproveRequestAsync(model.Id, reviewerId, reviewerName);
            return Json(new { success, message });
        }

        public class RejectRequestModel
        {
            public int Id { get; set; }
            public string Reason { get; set; } = string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Reject([FromBody] RejectRequestModel model)
        {
            if (!IsAdminOrManager())
            {
                return Forbid();
            }

            if (model == null || model.Id <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid request ID." });
            }

            if (string.IsNullOrWhiteSpace(model.Reason))
            {
                return Json(new { success = false, message = "Please provide a reason for rejecting this delete request." });
            }

            var reviewerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "1";
            var reviewerName = User.Identity?.Name ?? "Admin";

            var (success, message) = await _deleteRequestService.RejectRequestAsync(model.Id, model.Reason, reviewerId, reviewerName);
            return Json(new { success, message });
        }
    }
}
