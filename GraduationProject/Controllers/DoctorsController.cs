﻿using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDoctorManager manager;

        public DoctorsController(IHttpContextAccessor _httpContextAccessor, IDoctorManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<DoctorDTO>>> GetAll()
        {
            var doctors = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(doctors);
        }

        [AllowAnonymous]
        [HttpGet("DepartmentDoctors/{id:int}")]
        public async Task<ActionResult<List<DoctorDTO>>> GetDepartmentDoctors(int id)
        {
            var doctors = await manager.GetDepartmentDoctors(id,Utils.GetLang(httpContextAccessor));
            if(doctors!=null && doctors.Count >0)
                return Ok(doctors);
            return NotFound();
        }



        [HttpGet("InsertDTO/{id:int}")]
        public async Task<ActionResult<DoctorInsertDTO>> GetDoctorInsertDTO(int id)
        {
            var doctor = await manager.GetInsertDTOByIdAsync(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        [AllowAnonymous]
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
        public async Task<ActionResult> CreateDoctor([FromForm] DoctorFormData formData)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(formData);
                return Ok();
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
        public async Task<ActionResult<DoctorInsertDTO>> UpdateDoctor(int id, [FromForm] DoctorFormData formData)
        {
            await manager.UpdateAsync(id, formData);
            return Ok();
        }
    }
}
