using GraduationProject_BL.DTO.Admin;
using GraduationProject_BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminManager manager;

        public AdminController(IAdminManager _manager)
        {
            manager = _manager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AdminDTO>> Login(AdminLoginDTO login)
        {
            if (!string.IsNullOrEmpty(login.UserName) && !string.IsNullOrEmpty(login.Password))
            {

                var isFound = await manager.FindAdmin(login.UserName, login.Password);

                if (isFound)
                {
                    var dto = await manager.Login(login.UserName);

                    if (dto != null)
                    {
                        return Ok(dto);
                    }
                    else
                    {
                        return Unauthorized("Invalid Attempt!");
                    }
                }
                return Unauthorized("Incorrect username or password!");
            }
            else return BadRequest();
        }
    }
}
