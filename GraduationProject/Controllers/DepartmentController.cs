using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IRepository<Department> repository;

        public DepartmentController(IRepository<Department> _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Department>>> GetAll()
        {
            var departments = await repository.GetAllAsync();

            if (departments.Count == 0)
                return NotFound();

            return Ok(departments);
        }

        [HttpGet("/Department/{id:int}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var departments = await repository.GetAllAsync();

            if (departments != null)
            {
                var department = departments.Find(x => x.Id == id);
                if (department != null)
                {
                    return Ok(department);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(department);
                return Ok(department);
            }
            return BadRequest();
        }

        [HttpDelete("/Department/Delete/{id:int}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("/Department/Update/{id:int}")]
        public async Task<ActionResult<Department>> UpdateDepartment(int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();

            await repository.UpdateAsync(id, department);
            return Ok(department);
        }
    }
}
