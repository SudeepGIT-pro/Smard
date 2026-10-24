using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smard.Data;
using Smard.Models;
using Smard.Services;
using System.Security.Claims;

namespace Smard.Controllers
{
    [Authorize]
    public abstract class BaseMasterController<TMaster, TCostDetail> : Controller
        where TMaster : GenericMasterBase, IMasterWithCostDetails<TCostDetail>, new()
        where TCostDetail : GenericCostDetailBase, new()
    {
        protected readonly SmardDbContext _context;
        protected readonly string _masterName;

        protected BaseMasterController(SmardDbContext context, string masterName)
        {
            _context = context;
            _masterName = masterName;
        }

        public virtual async Task<IActionResult> Index()
        {
            var list = await _context.Set<TMaster>()
                .Include(m => m.CostDetails)
                .Where(m => m.IsActive)
                .OrderBy(m => m.Id)
                .ToListAsync();

            var dtos = list.Select(MapToDto).ToList();
            ViewBag.InitialMasterRecordsJson = System.Text.Json.JsonSerializer.Serialize(dtos, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });

            return View();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Save([FromBody] GenericMasterSaveRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest(new { success = false, message = "Invalid request payload." });
                }

                if (string.IsNullOrWhiteSpace(request.TypeName))
                {
                    return Json(new { success = false, message = "Type nam hhhhhhde is mandatory. Please enter a Type name." });
                }

                var trimmedName = request.TypeName.Trim();

                // Unique Type Name check
                var nameExists = await _context.Set<TMaster>()
                    .AnyAsync(m => m.IsActive && m.TypeName.ToLower() == trimmedName.ToLower());

                if (nameExists)
                {
                    return Json(new { success = false, message = $"Type '{trimmedName}' already exists in {_masterName}. Type name must be unique." });
                }

                // Total Rate must be greater than 0
                if (request.TotalRateAmount <= 0)
                {
                    return Json(new { success = false, message = "Total Rate cannot be 0.00. Please enter cost details before saving." });
                }

                request.TypeName = trimmedName;

                var userName = GetCurrentUserName();
                var record = MapToEntity(request);
                record.CreatedBy = userName;
                record.CreatedOn = DateTime.Now;
                record.UpdatedBy = null;
                record.UpdatedOn = null;

                foreach (var d in BuildDetails(request.MaterialCosts, "Material"))
                {
                    record.CostDetails.Add(d);
                }

                foreach (var d in BuildDetails(request.LabourCosts, "Labour"))
                {
                    record.CostDetails.Add(d);
                }

                await _context.Set<TMaster>().AddAsync(record);
                await _context.SaveChangesAsync();

                await NotifyMasterChangeAsync(record.TypeName, "Create");

                return Json(new
                {
                    success = true,
                    id = record.Id,
                    createdBy = record.CreatedBy,
                    createdOn = record.CreatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                    updatedBy = (string?)null,
                    updatedOn = (string?)null,
                    isUpdate = false,
                    message = $"{_masterName} '{record.TypeName}' saved successfully."
                });
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update([FromBody] GenericMasterSaveRequest request)
        {
            try
            {
                if (request?.Id is null or <= 0)
                {
                    return BadRequest(new { success = false, message = "Invalid ID." });
                }

                if (string.IsNullOrWhiteSpace(request.TypeName))
                {
                    return Json(new { success = false, message = "Type name is mandatory. Please enter a Type name." });
                }

                var trimmedName = request.TypeName.Trim();

                // Unique Type Name check (excluding current record being updated)
                var nameExists = await _context.Set<TMaster>()
                    .AnyAsync(m => m.IsActive && m.Id != request.Id.Value && m.TypeName.ToLower() == trimmedName.ToLower());

                if (nameExists)
                {
                    return Json(new { success = false, message = $"Type '{trimmedName}' already exists in {_masterName}. Type name must be unique." });
                }

                // Total Rate must be greater than 0
                if (request.TotalRateAmount <= 0)
                {
                    return Json(new { success = false, message = "Total Rate cannot be 0.00. Please enter cost details before saving." });
                }

                request.TypeName = trimmedName;

                var record = await _context.Set<TMaster>()
                    .Include(m => m.CostDetails)
                    .FirstOrDefaultAsync(m => m.Id == request.Id.Value);

                if (record == null)
                {
                    return NotFound(new { success = false, message = "Record not found." });
                }

                var hasChanges =
                    record.TypeName != trimmedName ||
                    record.SystemDescription != (request.SystemDescription ?? string.Empty).Trim() ||
                    record.DimensionLength != request.DimensionLength ||
                    record.DimensionHeight != request.DimensionHeight ||
                    record.DimensionArea != request.DimensionArea ||
                    record.MaterialTotalAmount != request.MaterialTotalAmount ||
                    record.LabourTotalAmount != request.LabourTotalAmount ||
                    record.TransportationPercent != request.TransportationPercent ||
                    record.TransportationRemarks != (request.TransportationRemarks ?? string.Empty).Trim() ||
                    record.WaterElectricityPercent != request.WaterElectricityPercent ||
                    record.WaterElectricityRemarks != (request.WaterElectricityRemarks ?? string.Empty).Trim() ||
                    record.ToolsEquipmentPercent != request.ToolsEquipmentPercent ||
                    record.ToolsEquipmentRemarks != (request.ToolsEquipmentRemarks ?? string.Empty).Trim() ||
                    record.ProfitPercent != request.ProfitPercent ||
                    record.TotalRateAmount != request.TotalRateAmount ||
                    record.IsActive != request.IsActive ||
                    AreCostDetailsChanged(record.CostDetails, request.MaterialCosts, request.LabourCosts);

                if (!hasChanges)
                {
                    return Json(new
                    {
                        success = true,
                        noChanges = true,
                        id = record.Id,
                        createdBy = record.CreatedBy,
                        createdOn = record.CreatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                        updatedBy = record.UpdatedBy,
                        updatedOn = record.UpdatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                        isUpdate = record.UpdatedOn.HasValue && record.UpdatedOn != record.CreatedOn && !string.IsNullOrWhiteSpace(record.UpdatedBy),
                        message = "No changes done to update."
                    });
                }

                ApplyUpdate(record, request);
                var userName = GetCurrentUserName();
                if (string.IsNullOrWhiteSpace(record.CreatedBy))
                {
                    record.CreatedBy = userName;
                }
                if (!record.CreatedOn.HasValue)
                {
                    record.CreatedOn = DateTime.Now;
                }
                record.UpdatedBy = userName;
                record.UpdatedOn = DateTime.Now;

                _context.Set<TCostDetail>().RemoveRange(record.CostDetails);
                record.CostDetails.Clear();

                foreach (var d in BuildDetails(request.MaterialCosts, "Material"))
                {
                    record.CostDetails.Add(d);
                }

                foreach (var d in BuildDetails(request.LabourCosts, "Labour"))
                {
                    record.CostDetails.Add(d);
                }

                await _context.SaveChangesAsync();

                await NotifyMasterChangeAsync(record.TypeName, "Update");

                return Json(new
                {
                    success = true,
                    id = record.Id,
                    createdBy = record.CreatedBy,
                    createdOn = record.CreatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                    updatedBy = record.UpdatedBy,
                    updatedOn = record.UpdatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
                    isUpdate = true,
                    message = $"{_masterName} '{record.TypeName}' updated successfully."
                });
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetById(int id)
        {
            var record = await _context.Set<TMaster>()
                .Include(m => m.CostDetails)
                .FirstOrDefaultAsync(m => m.Id == id);

            return record == null ? NotFound() : Json(MapToDto(record));
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var list = await _context.Set<TMaster>()
                .Include(m => m.CostDetails)
                .Where(m => m.IsActive)
                .OrderBy(m => m.Id)
                .ToListAsync();

            return Json(list.Select(MapToDto));
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteType([FromBody] DeleteTypeRequest request)
        {
            try
            {
                if (!IsAdminOrManager())
                {
                    return StatusCode(403, new { success = false, message = "Only Admins and Managers can delete specifications directly. Regular users must submit a delete request for approval." });
                }

                var record = await _context.Set<TMaster>().FindAsync(request.Id);
                if (record == null)
                {
                    return Json(new { success = false, message = "Record not found." });
                }

                var typeName = record.TypeName;
                _context.Set<TMaster>().Remove(record);

                // Auto-resolve any pending delete requests for this record
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                var pendingReqs = await _context.DeleteRequests
                    .Where(r => r.RecordId == request.Id && r.Status == "Pending" && (r.ControllerName == controllerName || r.ControllerName == controllerName.Replace("Controller", "")))
                    .ToListAsync();
                if (pendingReqs.Any())
                {
                    var reviewerId = GetCurrentUserId();
                    var reviewerName = GetCurrentUserName();
                    foreach (var pr in pendingReqs)
                    {
                        pr.Status = "Approved";
                        pr.ReviewedByUserId = reviewerId;
                        pr.ReviewedByUserName = reviewerName;
                        pr.ReviewedOn = DateTime.Now;
                    }
                }

                await _context.SaveChangesAsync();

                await NotifyMasterChangeAsync(typeName, "Delete");

                return Json(new
                {
                    success = true,
                    message = $"'{record.TypeName}' deleted successfully."
                });
            }
            catch (Exception ex)
            {
                return Error500(ex);
            }
        }

        protected async Task NotifyMasterChangeAsync(string typeName, string actionType)
        {
            try
            {
                var userName = GetCurrentUserName();
                var controllerName = ControllerContext.ActionDescriptor.ControllerName;
                var actionUrl = string.IsNullOrWhiteSpace(controllerName) ? null : $"/{controllerName}";
                var notifSvc = HttpContext.RequestServices.GetService<INotificationService>() ?? new NotificationService(_context);
                await notifSvc.CreateMasterNotificationAsync(_masterName, typeName, actionType, userName, actionUrl);
            }
            catch
            {
                // Never let notification logging fail the primary user operation
            }
        }

        protected bool IsAdminOrManager()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value ?? "User";
            var isAdmin = User.IsInRole("Admin") || (userRole == "Admin") || string.Equals(User.FindFirst(ClaimTypes.Email)?.Value, "sudeep@smard.com", StringComparison.OrdinalIgnoreCase);
            var isManager = User.IsInRole("Manager") || (userRole == "Manager");
            return isAdmin || isManager;
        }

        protected string GetCurrentUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value
                ?? "1";
        }

        protected string GetCurrentUserName()
        {
            return User.Identity?.Name ??
                   User.FindFirst(ClaimTypes.Name)?.Value ??
                   User.FindFirst(ClaimTypes.Email)?.Value ??
                   "Admin";
        }

        protected static TMaster MapToEntity(GenericMasterSaveRequest r) => new()
        {
            TypeName = r.TypeName.Trim(),
            SystemDescription = (r.SystemDescription ?? string.Empty).Trim(),
            DimensionLength = r.DimensionLength,
            DimensionHeight = r.DimensionHeight,
            DimensionSize = $"{r.DimensionLength:0.##} x {r.DimensionHeight:0.##}",
            DimensionArea = r.DimensionArea,
            MaterialTotalAmount = r.MaterialTotalAmount,
            MaterialTotalPerSquareMeter = r.MaterialTotalPerSquareMeter,
            LabourTotalAmount = r.LabourTotalAmount,
            LabourTotalPerSquareMeter = r.LabourTotalPerSquareMeter,
            GrandTotalPerSquareMeter = r.GrandTotalPerSquareMeter,
            TransportationPercent = r.TransportationPercent,
            TransportationAmount = r.TransportationAmount,
            TransportationRemarks = (r.TransportationRemarks ?? string.Empty).Trim(),
            WaterElectricityPercent = r.WaterElectricityPercent,
            WaterElectricityAmount = r.WaterElectricityAmount,
            WaterElectricityRemarks = (r.WaterElectricityRemarks ?? string.Empty).Trim(),
            ToolsEquipmentPercent = r.ToolsEquipmentPercent,
            ToolsEquipmentAmount = r.ToolsEquipmentAmount,
            ToolsEquipmentRemarks = (r.ToolsEquipmentRemarks ?? string.Empty).Trim(),
            ProfitPercent = r.ProfitPercent,
            ProfitAmount = r.ProfitAmount,
            TotalRateAmount = r.TotalRateAmount,
            IsActive = r.IsActive
        };

        protected static void ApplyUpdate(TMaster e, GenericMasterSaveRequest r)
        {
            e.TypeName = r.TypeName.Trim();
            e.SystemDescription = (r.SystemDescription ?? string.Empty).Trim();
            e.DimensionLength = r.DimensionLength;
            e.DimensionHeight = r.DimensionHeight;
            e.DimensionSize = $"{r.DimensionLength:0.##} x {r.DimensionHeight:0.##}";
            e.DimensionArea = r.DimensionArea;
            e.MaterialTotalAmount = r.MaterialTotalAmount;
            e.MaterialTotalPerSquareMeter = r.MaterialTotalPerSquareMeter;
            e.LabourTotalAmount = r.LabourTotalAmount;
            e.LabourTotalPerSquareMeter = r.LabourTotalPerSquareMeter;
            e.GrandTotalPerSquareMeter = r.GrandTotalPerSquareMeter;
            e.TransportationPercent = r.TransportationPercent;
            e.TransportationAmount = r.TransportationAmount;
            e.TransportationRemarks = (r.TransportationRemarks ?? string.Empty).Trim();
            e.WaterElectricityPercent = r.WaterElectricityPercent;
            e.WaterElectricityAmount = r.WaterElectricityAmount;
            e.WaterElectricityRemarks = (r.WaterElectricityRemarks ?? string.Empty).Trim();
            e.ToolsEquipmentPercent = r.ToolsEquipmentPercent;
            e.ToolsEquipmentAmount = r.ToolsEquipmentAmount;
            e.ToolsEquipmentRemarks = (r.ToolsEquipmentRemarks ?? string.Empty).Trim();
            e.ProfitPercent = r.ProfitPercent;
            e.ProfitAmount = r.ProfitAmount;
            e.TotalRateAmount = r.TotalRateAmount;
            e.IsActive = r.IsActive;
        }

        protected static object MapToDto(TMaster m) => new
        {
            id = m.Id,
            typeName = m.TypeName,
            systemDescription = m.SystemDescription,
            dimensionLength = m.DimensionLength,
            dimensionHeight = m.DimensionHeight,
            dimensionSize = m.DimensionSize,
            dimensionArea = m.DimensionArea,
            materialTotalAmount = m.MaterialTotalAmount,
            materialTotalPerSquareMeter = m.MaterialTotalPerSquareMeter,
            labourTotalAmount = m.LabourTotalAmount,
            labourTotalPerSquareMeter = m.LabourTotalPerSquareMeter,
            grandTotalPerSquareMeter = m.GrandTotalPerSquareMeter,
            transportationPercent = m.TransportationPercent,
            transportationAmount = m.TransportationAmount,
            transportationRemarks = m.TransportationRemarks,
            waterElectricityPercent = m.WaterElectricityPercent,
            waterElectricityAmount = m.WaterElectricityAmount,
            waterElectricityRemarks = m.WaterElectricityRemarks,
            toolsEquipmentPercent = m.ToolsEquipmentPercent,
            toolsEquipmentAmount = m.ToolsEquipmentAmount,
            toolsEquipmentRemarks = m.ToolsEquipmentRemarks,
            profitPercent = m.ProfitPercent,
            profitAmount = m.ProfitAmount,
            totalRateAmount = m.TotalRateAmount,
            isActive = m.IsActive,
            createdBy = string.IsNullOrWhiteSpace(m.CreatedBy) ? "Admin" : m.CreatedBy,
            createdOn = m.CreatedOn?.ToString("dd-MMM-yyyy hh:mm tt"),
            updatedBy = (m.UpdatedOn.HasValue && m.UpdatedOn != m.CreatedOn && !string.IsNullOrWhiteSpace(m.UpdatedBy)) ? m.UpdatedBy : null,
            updatedOn = (m.UpdatedOn.HasValue && m.UpdatedOn != m.CreatedOn) ? m.UpdatedOn?.ToString("dd-MMM-yyyy hh:mm tt") : null,
            isUpdate = m.UpdatedOn.HasValue && m.UpdatedOn != m.CreatedOn && !string.IsNullOrWhiteSpace(m.UpdatedBy),
            materialCosts = m.CostDetails.Where(d => d.CostType == "Material").OrderBy(d => d.SlNo).Select(d => new
            {
                slNo = d.SlNo,
                itemDescription = d.ItemDescription,
                specification = d.Specification,
                unit = d.Unit,
                qty = d.Qty,
                unitRate = d.UnitRate,
                amount = d.Amount,
                remarks = d.Remarks
            }),
            labourCosts = m.CostDetails.Where(d => d.CostType == "Labour").OrderBy(d => d.SlNo).Select(d => new
            {
                slNo = d.SlNo,
                itemDescription = d.ItemDescription,
                specification = d.Specification,
                unit = d.Unit,
                qty = d.Qty,
                unitRate = d.UnitRate,
                amount = d.Amount,
                remarks = d.Remarks
            })
        };

        protected static IEnumerable<TCostDetail> BuildDetails(IEnumerable<GenericCostDetailRequest>? items, string costType)
        {
            if (items == null) return Enumerable.Empty<TCostDetail>();

            return items
                .Where(d => !string.IsNullOrWhiteSpace(d.ItemDescription) ||
                            !string.IsNullOrWhiteSpace(d.Specification) ||
                            !string.IsNullOrWhiteSpace(d.Unit) ||
                            d.Qty > 0 ||
                            d.UnitRate > 0 ||
                            d.Amount > 0 ||
                            !string.IsNullOrWhiteSpace(d.Remarks))
                .Select(d => new TCostDetail
                {
                    CostType = costType,
                    SlNo = d.SlNo,
                    ItemDescription = (d.ItemDescription ?? string.Empty).Trim(),
                    Specification = (d.Specification ?? string.Empty).Trim(),
                    Unit = (d.Unit ?? string.Empty).Trim(),
                    Qty = d.Qty,
                    UnitRate = d.UnitRate,
                    Amount = d.Amount,
                    Remarks = (d.Remarks ?? string.Empty).Trim()
                })
                .ToList();
        }

        protected static bool AreCostDetailsChanged(
            ICollection<TCostDetail> existingDetails,
            IEnumerable<GenericCostDetailRequest>? newMaterialCosts,
            IEnumerable<GenericCostDetailRequest>? newLabourCosts)
        {
            var newMats = (newMaterialCosts ?? Enumerable.Empty<GenericCostDetailRequest>())
                .Where(d => !string.IsNullOrWhiteSpace(d.ItemDescription) ||
                            !string.IsNullOrWhiteSpace(d.Specification) ||
                            !string.IsNullOrWhiteSpace(d.Unit) ||
                            d.Qty > 0 || d.UnitRate > 0 || d.Amount > 0 ||
                            !string.IsNullOrWhiteSpace(d.Remarks))
                .ToList();
            var existingMats = existingDetails.Where(d => d.CostType == "Material").OrderBy(d => d.SlNo).ToList();

            if (newMats.Count != existingMats.Count) return true;

            for (int i = 0; i < newMats.Count; i++)
            {
                var n = newMats[i];
                var o = existingMats[i];
                if ((n.ItemDescription ?? string.Empty).Trim() != (o.ItemDescription ?? string.Empty).Trim() ||
                    (n.Specification ?? string.Empty).Trim() != (o.Specification ?? string.Empty).Trim() ||
                    (n.Unit ?? string.Empty).Trim() != (o.Unit ?? string.Empty).Trim() ||
                    n.Qty != o.Qty ||
                    n.UnitRate != o.UnitRate ||
                    n.Amount != o.Amount ||
                    (n.Remarks ?? string.Empty).Trim() != (o.Remarks ?? string.Empty).Trim())
                {
                    return true;
                }
            }

            var newLabs = (newLabourCosts ?? Enumerable.Empty<GenericCostDetailRequest>())
                .Where(d => !string.IsNullOrWhiteSpace(d.ItemDescription) ||
                            !string.IsNullOrWhiteSpace(d.Specification) ||
                            !string.IsNullOrWhiteSpace(d.Unit) ||
                            d.Qty > 0 || d.UnitRate > 0 || d.Amount > 0 ||
                            !string.IsNullOrWhiteSpace(d.Remarks))
                .ToList();
            var existingLabs = existingDetails.Where(d => d.CostType == "Labour").OrderBy(d => d.SlNo).ToList();

            if (newLabs.Count != existingLabs.Count) return true;

            for (int i = 0; i < newLabs.Count; i++)
            {
                var n = newLabs[i];
                var o = existingLabs[i];
                if ((n.ItemDescription ?? string.Empty).Trim() != (o.ItemDescription ?? string.Empty).Trim() ||
                    (n.Specification ?? string.Empty).Trim() != (o.Specification ?? string.Empty).Trim() ||
                    (n.Unit ?? string.Empty).Trim() != (o.Unit ?? string.Empty).Trim() ||
                    n.Qty != o.Qty ||
                    n.UnitRate != o.UnitRate ||
                    n.Amount != o.Amount ||
                    (n.Remarks ?? string.Empty).Trim() != (o.Remarks ?? string.Empty).Trim())
                {
                    return true;
                }
            }

            return false;
        }

        protected IActionResult Error500(Exception ex) => StatusCode(500, new
        {
            success = false,
            message = ex.InnerException?.Message ?? ex.Message
        });
    }
}
