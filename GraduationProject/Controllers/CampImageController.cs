using GraduationProject_BL.DTO.CampImageDTOs;
using GraduationProject_BL.DTO.DepartmentDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_BL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CampImageController : ControllerBase
    {
        private readonly ICampImageManager manager;
        public CampImageController(ICampImageManager _manager)
        {
            manager = _manager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CampImageDTO>>> GetAll()
        {
            var departments = await manager.GetAllAsync();

            return Ok(departments);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CampImageDTO>> GetById(int id)
        {
            var image = await manager.GetByIdAsync(id);

            if (image != null)
            {
                return Ok(image);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CampImageInsertDTO>> CreateCampImage([FromForm] CampImageInsertDTO campImage)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(campImage);
                return Ok(campImage);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCampImage(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }
    }
}
