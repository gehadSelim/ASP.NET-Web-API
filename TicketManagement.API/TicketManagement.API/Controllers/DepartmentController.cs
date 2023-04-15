using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.BL;

namespace TicketManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentManager _deptManager;
        public DepartmentController(IDepartmentManager deptManager)
        {
            _deptManager = deptManager;
        }

        [HttpGet("tdev")]
        public ActionResult<IEnumerable<DeptWithTicketsDto>> GetWithTicketAndDevNo() 
        {
            return _deptManager.GetDeptwithTicketsAndDevCount();
        }
    }
}
