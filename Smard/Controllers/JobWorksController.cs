using Microsoft.AspNetCore.Authorization;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    public class JobWorksController : BaseMasterController<JobWorksMaster, JobWorksCostDetail>
    {
        public JobWorksController(SmardDbContext context) : base(context, "Job Works")
        {
        }
    }
}
