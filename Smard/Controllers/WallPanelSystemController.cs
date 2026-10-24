using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class WallPanelSystemController : BaseMasterController<WallPanelSystemMaster, WallPanelSystemCostDetail>
    {
        public WallPanelSystemController(SmardDbContext context) : base(context, "Wall Panel System")
        {
        }
    }
}
