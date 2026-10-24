using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class CountersController : BaseMasterController<CountersMaster, CountersCostDetail>
    {
        public CountersController(SmardDbContext context) : base(context, "Counters")
        {
        }
    }
}
