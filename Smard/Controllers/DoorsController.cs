using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class DoorsController : BaseMasterController<DoorsMaster, DoorsCostDetail>
    {
        public DoorsController(SmardDbContext context) : base(context, "Doors")
        {
        }
    }
}
