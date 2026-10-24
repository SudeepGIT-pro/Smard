using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class SinkController : BaseMasterController<SinkMaster, SinkCostDetail>
    {
        public SinkController(SmardDbContext context) : base(context, "Sink")
        {
        }
    }
}
