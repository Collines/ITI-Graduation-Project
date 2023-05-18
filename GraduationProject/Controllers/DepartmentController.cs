using GraduationProject_BL.DTO;
using GraduationProject_BL.Managers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDepartmentManager manager;

        public DepartmentController(IHttpContextAccessor _httpContextAccessor, IDepartmentManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartmentDTO>>> GetAll()
        {
            var departments = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            if (departments.Count == 0)
                return NotFound();

            return Ok(departments);
        }

        [HttpGet("/Department/{id:int}")]
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
        public async Task<ActionResult<InsertDepartmentDTO>> CreateDepartment(InsertDepartmentDTO department)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(department);
                return Ok(department);
            }
            return BadRequest();
        }

        [HttpDelete("/Department/Delete/{id:int}")]
        public async Task<ActionResult<int>> DeleteDepartment(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPut("/Department/Update/{id:int}")]
        public async Task<ActionResult<InsertDepartmentDTO>> UpdateDepartment(int id, InsertDepartmentDTO department)
        {
            await manager.UpdateAsync(id, department);
            return Ok(department);
        }
    }
}
