using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class LGSPanelSystemController : BaseMasterController<LGSPanelSystemMaster, LGSPanelSystemCostDetail>
    {
        public LGSPanelSystemController(SmardDbContext context) : base(context, "LGS Panel System")
        {
        }
    }
}
