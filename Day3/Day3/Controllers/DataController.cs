using Day3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Day3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public DataController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetUserInfo()
        {
            var user = await _userManager.GetUserAsync(User); 

            return Ok(new
            {
                UserName = user?.UserName,
                Email = user?.Email,
                Department = user?.Department
            });
        }

        [HttpGet("for manager")]
        [Authorize(Policy = "AdminsOnly")]
        public ActionResult GetInfoForManager()
        {
            return Ok("Info for admin");
        }

        [HttpGet("for both")]
        [Authorize(Policy = "UsersOnly")]
        public ActionResult GetInfoForUser()
        {
            return Ok("Info for admin or user");
        }

    }
}
