using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Smard.Data;
using Smard.Models;

namespace Smard.Controllers
{
    [Authorize]
    [Route("CSSDAFurnitures")]
    [Route("CSSDFurnitures")]
    public class CSSDAFurnituresController : BaseMasterController<CSSDFurnituresMaster, CSSDFurnituresCostDetail>
    {
        public CSSDAFurnituresController(SmardDbContext context) : base(context, "CSSD Furnitures")
        {
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public override async Task<IActionResult> Index()
        {
            await base.Index();
            return View("~/Views/CSSDAFurnitures/Index.cshtml");
        }

        [HttpPost("Save")]
        public override Task<IActionResult> Save([FromBody] GenericMasterSaveRequest request) => base.Save(request);

        [HttpPost("Update")]
        public override Task<IActionResult> Update([FromBody] GenericMasterSaveRequest request) => base.Update(request);

        [HttpGet("GetById")]
        public override Task<IActionResult> GetById(int id) => base.GetById(id);

        [HttpGet("GetAll")]
        public override Task<IActionResult> GetAll() => base.GetAll();

        [HttpPost("DeleteType")]
        public override Task<IActionResult> DeleteType([FromBody] DeleteTypeRequest request) => base.DeleteType(request);
    }
}
