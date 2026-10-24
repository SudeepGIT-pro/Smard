using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class FixedFurnituresController : BaseMasterController<FixedFurnituresMaster, FixedFurnituresCostDetail>
    {
        public FixedFurnituresController(SmardDbContext context) : base(context, "Fixed Furnitures")
        {
        }
    }
}
