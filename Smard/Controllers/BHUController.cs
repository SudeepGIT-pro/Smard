using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class BHUController : BaseMasterController<BHUMaster, BHUCostDetail>
    {
        public BHUController(SmardDbContext context) : base(context, "BHU")
        {
        }
    }
}
