using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo DeptRepo;
        // private readonly GeneralRepo<Department> patientRepo;

        //public DepartmentController(GeneralRepo<Department> _repo)
        //{
        //    DeptRepo = _repo;
        //}

        public DepartmentController(IDepartmentRepo _repo)
        {
            DeptRepo = _repo;
        }

        [HttpGet]
        public ActionResult<List<Department>> GetAll()
        {
            var dept = DeptRepo.GetAllDept();

            if (dept.Count == 0)
                return NotFound();

            return Ok(dept);
        }

        [HttpGet("/Department/{id:int}")]
        public ActionResult<Department> GetById(int id)
        {
            if (id == null)
                return BadRequest();

            var dept = DeptRepo.GetDeptDetails(id);

            if (dept == null)
                return NotFound();
            return Ok(dept);
        }

        [HttpPost]
        public ActionResult<Department> CreateDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {
                DeptRepo.InsertDept(dept);
                return Ok(dept);
            }
            return BadRequest();
        }

        [HttpDelete("/Department/Delete/{id:int}")]
        public ActionResult<Doctor> DeleteDoctor(int id)
        {
            var dept = DeptRepo.GetDeptDetails(id);

            if (dept == null)
                return NotFound();

            DeptRepo.DeleteDept(id);
            return Ok(dept);
        }

        [HttpPut("/Department/Update/{id:int}")]
        public ActionResult<Department> UpdateDoctor(int id, Department dept)
        {
            if (id != dept.Id)
                return BadRequest();
            var updatedDepartment = DeptRepo.GetDeptDetails(id);

            if (updatedDepartment == null)
                return NotFound();
            DeptRepo.UpdateDept(id, dept);

            return Ok(dept);
        }
    }
}
