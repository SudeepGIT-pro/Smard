using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class LaminarFlowController : BaseMasterController<LaminarFlowMaster, LaminarFlowCostDetail>
    {
        public LaminarFlowController(SmardDbContext context) : base(context, "Laminar Flow")
        {
        }
    }
}
