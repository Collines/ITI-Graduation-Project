using GraduationProject_BL.DTO;
using GraduationProject_BL.Interfaces;
using GraduationProject_BL.Managers;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DoctorsController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDoctorManager manager;

        public DoctorsController(IHttpContextAccessor _httpContextAccessor, IDoctorManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [HttpGet]
        public async Task<ActionResult<List<DoctorDTO>>> GetAll()
        {
            var doctors = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            if (doctors.Count == 0)
                return NotFound();

            return Ok(doctors);
        }

        [HttpGet("InsertDTO/{id:int}")]
        public async Task<ActionResult<DoctorInsertDTO>> GetAllInsertDTO(int id)
        {
            var doctor = await manager.GetInsertDTOByIdAsync(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<DoctorDTO>> GetById(int id)
        {
            var doctor = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));

            if (doctor != null)
            {
                return Ok(doctor);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<DoctorInsertDTO>> CreateDoctor(DoctorInsertDTO doctor)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(doctor);
                return Ok(doctor);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteDoctor(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<DoctorInsertDTO>> UpdateDoctor(int id, DoctorInsertDTO doctor)
        {
            if (id != doctor.Id)
                return BadRequest();

            await manager.UpdateAsync(id, doctor);
            return Ok(doctor);
        }
    }
}
