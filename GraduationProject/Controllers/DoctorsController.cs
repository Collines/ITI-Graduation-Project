using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IRepository<Doctor> repository;

        public DoctorsController(IRepository<Doctor> _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            var doctors = await repository.GetAllAsync();

            if (doctors.Count == 0)
                return NotFound();

            return Ok(doctors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctors = await repository.GetAllAsync();

            if (doctors != null)
            {
                var doctor = doctors.Find(x => x.Id == id);
                if (doctor != null)
                {
                    return Ok(doctor);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(doctor);
                return Ok(doctor);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
                return BadRequest();

            await repository.UpdateAsync(id, doctor);
            return Ok(doctor);
        }
    }
}
