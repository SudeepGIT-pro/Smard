using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class LooseFurnituresController : BaseMasterController<LooseFurnituresMaster, LooseFurnituresCostDetail>
    {
        public LooseFurnituresController(SmardDbContext context) : base(context, "Loose Furnitures")
        {
        }
    }
}
