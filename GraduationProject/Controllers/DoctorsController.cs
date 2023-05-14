using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public ActionResult<List<Doctor>> GetAll()
        {
            var doctors = doctorRepo.GetAllDoctors();

            if (doctors.Count == 0)
                return NotFound();

            return Ok(doctors);
        }

        [HttpGet("/Doctors/{id:int}")]
        public ActionResult<Doctor> GetById(int id)
        {
            if (id == null)
                return BadRequest();

            var doctor = doctorRepo.GetDoctorDetails(id);

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

        [HttpDelete("/Doctors/Delete/{id:int}")]
        public ActionResult<Doctor> DeleteDoctor(int id)
        {
            var doctor = doctorRepo.GetDoctorDetails(id);

            if (doctor == null)
                return NotFound();

            doctorRepo.DeleteDoctor(id);
            return Ok(doctor);
        }

        [HttpPut("/Doctors/Update/{id:int}")]
        public ActionResult<Doctor> UpdateDoctor(int id, Doctor doctor)
        {
            if (id != doctor.Id)
                return BadRequest();
            var updatedDoctor = doctorRepo.GetDoctorDetails(id);

            if (updatedDoctor == null)
                return NotFound();
            doctorRepo.UpdateDoctor(id, doctor);

            return Ok(doctor);
        }
    }
}
