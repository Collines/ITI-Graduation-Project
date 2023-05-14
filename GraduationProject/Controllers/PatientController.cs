using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepo patientRepo;

        public PatientController(IPatientRepo Prepo)
        {
            patientRepo = Prepo;
        }

        [HttpGet]
        public ActionResult<List<Patient>> GetAll()
        {
            var patients = patientRepo.GetAllPatients();
            if (patients.Count == 0)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpGet("/Patient/{id}")]
        public ActionResult<Patient> GetPatientDetails(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var patients = patientRepo.GetPatientDetails(id);
            if (patients == null)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                patientRepo.InsertPatient(patient);
                return Ok(patient);
            }
            return BadRequest();
        }
        [HttpDelete("/Patient/Delete/{id}")]
        public ActionResult<Patient> DeletePatient(int id) 
        {
            var patients = patientRepo.GetPatientDetails(id);
            if(patients == null)
            {
                return NotFound();
            }
            patientRepo.DeletePatient(id);
            return Ok(patients);
        }
        [HttpPut("/Patient/Update/{id}")]
        public ActionResult <Patient> UpdatePatient(int id,Patient patient)
        {
            if (ModelState.IsValid)
            {
               if(id != patient.Id)
                {
                    return BadRequest();
                }
                var updatePatient = patientRepo.GetPatientDetails(id);
                if (updatePatient == null)
                {
                    return NotFound();
                }
                patientRepo.UpdatePatient(id, updatePatient);
                return Ok(updatePatient);

            }
            return NotFound();
        }




    }
}
