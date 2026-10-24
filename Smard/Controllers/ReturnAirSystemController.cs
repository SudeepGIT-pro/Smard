using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class ReturnAirSystemController : BaseMasterController<ReturnAirSystemMaster, ReturnAirSystemCostDetail>
    {
        public ReturnAirSystemController(SmardDbContext context) : base(context, "Return Air System")
        {
        }
    }
}
