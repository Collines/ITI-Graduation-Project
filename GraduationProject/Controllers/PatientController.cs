using Microsoft.AspNetCore.Mvc;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using GraduationProject_DAL.Data.DTO;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo patientRepo;
		private readonly IJWTManagerRepository JWTManager;

		public PatientController(IPatientRepo Prepo, IJWTManagerRepository jWTManager)
        {
            patientRepo = Prepo;
			JWTManager = jWTManager;
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

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatientDetails(int id)
        {
            var patients = patientRepo.GetPatientDetails(id);
            if (patients == null)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpPost("Register")]
		[AllowAnonymous]
		public ActionResult<Patient> Register(Patient patient)
        {
            if (patientRepo.IsExist(patient.Email))
                ModelState.AddModelError("Email", "Email Already Exist");
            if (ModelState.IsValid)
            {
                patient.Password = PasswordHandler.Hash(patient.Password, out byte[] salt);
                patient.PasswordSalt = Convert.ToHexString(salt);
                patientRepo.InsertPatient(patient);
                return Ok(JWTManager.Authenticate(patient));
            }
            return BadRequest("Email Already Exist");
        }
        [HttpDelete("{id}")]
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
        [HttpPatch("{id}")]
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

		[HttpPost("Login")]
        [AllowAnonymous]
		public ActionResult<Patient> Login(PatientLoginDTO p)
		{
            if (!String.IsNullOrEmpty(p.Email) && !String.IsNullOrEmpty(p.Password))
            {
                Patient patient = new Patient() { Email = p.Email, Password = p.Password };
                Token? token = JWTManager.Authenticate(patient);
				if (token != null)
				{
					return Ok(token);
				}
				else return Unauthorized();
			}
            else return BadRequest();
		}
	}
}
