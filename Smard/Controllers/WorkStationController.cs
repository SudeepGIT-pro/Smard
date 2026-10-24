using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class WorkStationController : BaseMasterController<WorkStationMaster, WorkStationCostDetail>
    {
        public WorkStationController(SmardDbContext context) : base(context, "Work Station")
        {
        }
    }
}
