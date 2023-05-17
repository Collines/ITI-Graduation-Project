using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepo doctorRepo;
        // private readonly GeneralRepo<Docotr> patientRepo;

        //public DoctorsController(GeneralRepo<Doctor> _repo)
        //{
        //    doctorRepo = _repo;
        //}
        public DoctorsController(IDoctorRepo _repo)
        {
            doctorRepo = _repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            var doctors = await doctorRepo.GetAllDoctors();

            if (doctors.Count == 0)
                return NotFound();

            return Ok(doctors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctor = await doctorRepo.GetDoctorDetails(id);

            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public ActionResult<Doctor> CreateDoctor(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                doctorRepo.InsertDoctor(doctor);
                return Ok(doctor);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Doctor>> DeleteDoctor(int id)
        {
            var doctor = await doctorRepo.GetDoctorDetails(id);

            if (doctor == null)
                return NotFound();

            doctorRepo.DeleteDoctor(id);
            return Ok(doctor);
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
                return BadRequest();
            var updatedDoctor = await doctorRepo.GetDoctorDetails(id);

            if (updatedDoctor == null)
                return NotFound();
            doctorRepo.UpdateDoctor(id, doctor);

            return Ok(doctor);
        }
    }
}
