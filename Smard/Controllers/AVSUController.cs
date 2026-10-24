using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class AVSUController : BaseMasterController<AVSUMaster, AVSUCostDetail>
    {
        public AVSUController(SmardDbContext context) : base(context, "AVSU")
        {
        }
    }
}
