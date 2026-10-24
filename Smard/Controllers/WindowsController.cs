using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class WindowsController : BaseMasterController<WindowsMaster, WindowsCostDetail>
    {
        public WindowsController(SmardDbContext context) : base(context, "Windows")
        {
        }
    }
}
