using Microsoft.EntityFrameworkCore;
using Smard.Data;
using Smard.Models;

namespace Smard.Services
{
    public class DeleteRequestDto
    {
        public int Id { get; set; }
        public string RequestedByUserId { get; set; } = string.Empty;
        public string RequestedByUserName { get; set; } = string.Empty;
        public string MasterName { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public int RecordId { get; set; }
        public int TypeIndex { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public string RequestedOn { get; set; } = string.Empty;
        public string? ReviewedByUserName { get; set; }
        public string? ReviewedOn { get; set; }
        public string? RejectReason { get; set; }
        public string? ActionUrl { get; set; }
    }

    public class CreateDeleteRequestInput
    {
        public string MasterName { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public int RecordId { get; set; }
        public int TypeIndex { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? ActionUrl { get; set; }
    }

    public interface IDeleteRequestService
    {
        Task<int> GetPendingCountAsync();
        Task<List<DeleteRequestDto>> GetPendingRequestsAsync();
        Task<List<DeleteRequestDto>> GetUserRequestsAsync(string userId);
        Task<List<int>> GetPendingRecordIdsAsync(string controllerName);
        Task<(bool Success, string Message, int Id)> CreateRequestAsync(CreateDeleteRequestInput input, string userId, string userName);
        Task<(bool Success, string Message)> ApproveRequestAsync(int id, string reviewerId, string reviewerName);
        Task<(bool Success, string Message)> RejectRequestAsync(int id, string reason, string reviewerId, string reviewerName);
    }

    public class DeleteRequestService : IDeleteRequestService
    {
        private readonly SmardDbContext _context;
        private readonly INotificationService _notificationService;

        public DeleteRequestService(SmardDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<int> GetPendingCountAsync()
        {
            return await _context.DeleteRequests.CountAsync(r => r.Status == "Pending");
        }

        public async Task<List<DeleteRequestDto>> GetPendingRequestsAsync()
        {
            var list = await _context.DeleteRequests
                .Where(r => r.Status == "Pending")
                .OrderByDescending(r => r.RequestedOn)
                .ToListAsync();

            return list.Select(MapToDto).ToList();
        }

        public async Task<List<DeleteRequestDto>> GetUserRequestsAsync(string userId)
        {
            var list = await _context.DeleteRequests
                .Where(r => r.RequestedByUserId == userId)
                .OrderByDescending(r => r.RequestedOn)
                .ToListAsync();

            return list.Select(MapToDto).ToList();
        }

        public async Task<List<int>> GetPendingRecordIdsAsync(string controllerName)
        {
            var ctrl = (controllerName ?? string.Empty).Trim().ToLowerInvariant();
            return await _context.DeleteRequests
                .Where(r => r.Status == "Pending" && r.ControllerName.ToLower() == ctrl && r.RecordId > 0)
                .Select(r => r.RecordId)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message, int Id)> CreateRequestAsync(CreateDeleteRequestInput input, string userId, string userName)
        {
            try
            {
                var ctrl = (input.ControllerName ?? string.Empty).Trim().ToLowerInvariant();
                var trimmedTypeName = (input.TypeName ?? string.Empty).Trim().ToLowerInvariant();

                // Check if a pending delete request already exists for this master record / type
                var existingPending = await _context.DeleteRequests
                    .FirstOrDefaultAsync(r => r.Status == "Pending" &&
                        r.ControllerName.ToLower() == ctrl &&
                        (
                            (input.RecordId > 0 && r.RecordId == input.RecordId) ||
                            (input.TypeIndex > 0 && r.TypeIndex == input.TypeIndex) ||
                            (!string.IsNullOrEmpty(trimmedTypeName) && r.TypeName.ToLower() == trimmedTypeName)
                        ));

                if (existingPending != null)
                {
                    return (false, "Request already submitted. A delete request for this specification is already pending review.", existingPending.Id);
                }

                var req = new DeleteRequest
                {
                    RequestedByUserId = userId,
                    RequestedByUserName = string.IsNullOrWhiteSpace(userName) ? "User" : userName,
                    MasterName = input.MasterName ?? string.Empty,
                    ControllerName = input.ControllerName ?? string.Empty,
                    RecordId = input.RecordId,
                    TypeIndex = input.TypeIndex,
                    TypeName = input.TypeName ?? string.Empty,
                    Status = "Pending",
                    RequestedOn = DateTime.Now,
                    ActionUrl = input.ActionUrl ?? $"/{input.ControllerName}"
                };

                await _context.DeleteRequests.AddAsync(req);
                await _context.SaveChangesAsync();

                // Notify Admins and Managers about the new delete request
                await _notificationService.CreateMasterNotificationAsync(
                    input.MasterName ?? "Master",
                    $"TYPE {input.TypeIndex} — \"{input.TypeName}\"",
                    "DeleteRequest",
                    userName,
                    input.ActionUrl ?? $"/{input.ControllerName}"
                );

                return (true, "Delete request submitted successfully for Admin/Manager approval.", req.Id);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, 0);
            }
        }

        public async Task<(bool Success, string Message)> ApproveRequestAsync(int id, string reviewerId, string reviewerName)
        {
            try
            {
                var req = await _context.DeleteRequests.FindAsync(id);
                if (req == null)
                {
                    return (false, "Delete request not found.");
                }

                if (req.Status != "Pending")
                {
                    return (false, $"This delete request has already been {req.Status.ToLower()}.");
                }

                // Delete the master record from the database if recordId is provided
                if (req.RecordId > 0)
                {
                    await DeleteMasterRecordAsync(req.ControllerName, req.RecordId);
                }

                req.Status = "Approved";
                req.ReviewedByUserId = reviewerId;
                req.ReviewedByUserName = reviewerName;
                req.ReviewedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                // Send notification to the requesting user
                await _notificationService.CreateUserNotificationAsync(
                    req.RequestedByUserId,
                    $"{req.MasterName} Delete Request Approved",
                    $"Your request to delete TYPE {req.TypeIndex} '\"{req.TypeName}\"' in {req.MasterName} has been approved by {reviewerName}.",
                    "Delete",
                    req.ActionUrl ?? $"/{req.ControllerName}"
                );

                return (true, $"Delete request for '{req.TypeName}' approved and removed.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool Success, string Message)> RejectRequestAsync(int id, string reason, string reviewerId, string reviewerName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(reason))
                {
                    return (false, "A reason for rejection is required.");
                }

                var req = await _context.DeleteRequests.FindAsync(id);
                if (req == null)
                {
                    return (false, "Delete request not found.");
                }

                if (req.Status != "Pending")
                {
                    return (false, $"This delete request has already been {req.Status.ToLower()}.");
                }

                req.Status = "Rejected";
                req.RejectReason = reason.Trim();
                req.ReviewedByUserId = reviewerId;
                req.ReviewedByUserName = reviewerName;
                req.ReviewedOn = DateTime.Now;

                await _context.SaveChangesAsync();

                // Send notification to the requesting user with the rejection reason
                await _notificationService.CreateUserNotificationAsync(
                    req.RequestedByUserId,
                    $"{req.MasterName} Delete Request Rejected",
                    $"Your request to delete TYPE {req.TypeIndex} '\"{req.TypeName}\"' in {req.MasterName} was rejected by {reviewerName}. Reason: \"{reason.Trim()}\"",
                    "Reject",
                    req.ActionUrl ?? $"/{req.ControllerName}"
                );

                return (true, "Delete request rejected successfully.");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private async Task DeleteMasterRecordAsync(string? controllerName, int recordId)
        {
            if (recordId <= 0 || string.IsNullOrWhiteSpace(controllerName)) return;

            var ctrl = controllerName.Trim().ToLowerInvariant();
            switch (ctrl)
            {
                case "wallpanelsystem":
                    var w = await _context.WallPanelSystemMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (w != null) _context.WallPanelSystemMasters.Remove(w);
                    break;
                case "ceilingsystem":
                    var c = await _context.CeilingSystemMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (c != null) _context.CeilingSystemMasters.Remove(c);
                    break;
                case "peripherallight":
                    var pl = await _context.PeripheralLightMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (pl != null) _context.PeripheralLightMasters.Remove(pl);
                    break;
                case "laminarflow":
                    var lf = await _context.LaminarFlowMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (lf != null) _context.LaminarFlowMasters.Remove(lf);
                    break;
                case "avsu":
                    var a = await _context.AVSUMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (a != null) _context.AVSUMasters.Remove(a);
                    break;
                case "doors":
                    var d = await _context.DoorsMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (d != null) _context.DoorsMasters.Remove(d);
                    break;
                case "storagecabinet":
                    var sc = await _context.StorageCabinetMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (sc != null) _context.StorageCabinetMasters.Remove(sc);
                    break;
                case "sink":
                    var sk = await _context.SinkMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (sk != null) _context.SinkMasters.Remove(sk);
                    break;
                case "workstation":
                    var ws = await _context.WorkStationMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (ws != null) _context.WorkStationMasters.Remove(ws);
                    break;
                case "bhu":
                    var b = await _context.BHUMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (b != null) _context.BHUMasters.Remove(b);
                    break;
                case "returnairsystem":
                    var r = await _context.ReturnAirSystemMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (r != null) _context.ReturnAirSystemMasters.Remove(r);
                    break;
                case "windows":
                    var win = await _context.WindowsMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (win != null) _context.WindowsMasters.Remove(win);
                    break;
                case "counters":
                    var cnt = await _context.CountersMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (cnt != null) _context.CountersMasters.Remove(cnt);
                    break;
                case "lgspanelsystem":
                    var lgs = await _context.LGSPanelSystemMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (lgs != null) _context.LGSPanelSystemMasters.Remove(lgs);
                    break;
                case "fixedfurnitures":
                    var ff = await _context.FixedFurnituresMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (ff != null) _context.FixedFurnituresMasters.Remove(ff);
                    break;
                case "loosefurnitures":
                    var lf2 = await _context.LooseFurnituresMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (lf2 != null) _context.LooseFurnituresMasters.Remove(lf2);
                    break;
                case "cssdfurnitures":
                case "cssdafurnitures":
                    var cf = await _context.CSSDFurnituresMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (cf != null) _context.CSSDFurnituresMasters.Remove(cf);
                    break;
                case "jobworks":
                    var jw = await _context.JobWorksMasters.Include(x => x.CostDetails).FirstOrDefaultAsync(x => x.Id == recordId);
                    if (jw != null) _context.JobWorksMasters.Remove(jw);
                    break;
            }
        }

        private static DeleteRequestDto MapToDto(DeleteRequest r)
        {
            return new DeleteRequestDto
            {
                Id = r.Id,
                RequestedByUserId = r.RequestedByUserId,
                RequestedByUserName = r.RequestedByUserName,
                MasterName = r.MasterName,
                ControllerName = r.ControllerName,
                RecordId = r.RecordId,
                TypeIndex = r.TypeIndex,
                TypeName = r.TypeName,
                Status = r.Status,
                RequestedOn = r.RequestedOn.ToString("dd-MMM-yyyy hh:mm tt"),
                ReviewedByUserName = r.ReviewedByUserName,
                ReviewedOn = r.ReviewedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                RejectReason = r.RejectReason,
                ActionUrl = r.ActionUrl
            };
        }
    }
}
