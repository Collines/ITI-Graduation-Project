using GraduationProject_BL.DTO.DepartmentDTOs;
using GraduationProject_BL.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDepartmentManager manager;

        public DepartmentController(IHttpContextAccessor _httpContextAccessor, IDepartmentManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<DepartmentDTO>>> GetAll()
        {
            var departments = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(departments);
        }
        [HttpGet("InsertDTO/{id:int}")]
        public async Task<ActionResult<DepartmentInsertDTO>> GetAllInsertDTO(int id) 
        {
            var department = await manager.GetInsertDTOByIdAsync(id);

            if (department == null)
                return NotFound();

            return Ok(department);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentDTO>> GetById(int id)
        {
            var department = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));

            if (department != null)
            {
                return Ok(department);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentInsertDTO>> CreateDepartment([FromForm] DepartmentInsertDTO department)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(department);
                return Ok(department);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteDepartment(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<DepartmentInsertDTO>> UpdateDepartment(int id,[FromForm] DepartmentInsertDTO department)
        {
            await manager.UpdateAsync(id, department);
            return Ok(department);
        }
    }
}
