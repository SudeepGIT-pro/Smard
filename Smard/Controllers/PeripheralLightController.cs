using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class PeripheralLightController : BaseMasterController<PeripheralLightMaster, PeripheralLightCostDetail>
    {
        public PeripheralLightController(SmardDbContext context) : base(context, "Peripheral Light")
        {
        }
    }
}
