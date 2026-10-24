using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class CeilingSystemController : BaseMasterController<CeilingSystemMaster, CeilingSystemCostDetail>
    {
        public CeilingSystemController(SmardDbContext context) : base(context, "Ceiling System")
        {
        }
    }
}
