
using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class StorageCabinetController : BaseMasterController<StorageCabinetMaster, StorageCabinetCostDetail>
    {
        public StorageCabinetController(SmardDbContext context) : base(context, "Storage Cabinet")
        {
        }
    }
}
