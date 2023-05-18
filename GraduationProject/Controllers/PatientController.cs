using GraduationProject_DAL.Data.DTO;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Interfaces.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IRepository<Patient> repository;
        private readonly IJWTManagerRepository JWTManager;
        private readonly IPatientServiceRepository PatientService;


        public PatientController(IRepository<Patient> _repository, IJWTManagerRepository jWTManager, IPatientServiceRepository patientService)
        {
            repository = _repository;
            JWTManager = jWTManager;
            PatientService = patientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await repository.GetAllAsync();
            if (patients.Count == 0)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientDetails(int id)
        {
            var patients = await repository.GetAllAsync();

            if (patients != null)
            {
                var patient = patients.Find(x => x.Id == id);
                if (patient != null)
                {
                    return Ok(patient);
                }
            }

            return NotFound();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<Patient>> Register(Patient patient)
        {
            if (await IsExists(patient.Email))
                ModelState.AddModelError("Email", "Email Already Exist");

            if (ModelState.IsValid)
            {
                patient.Password = PasswordHandler.Hash(patient.Password, out byte[] salt);
                patient.PasswordSalt = Convert.ToHexString(salt);
                await repository.InsertAsync(patient);
                var token = JWTManager.GenerateToken(patient);

                if (token == null)
                    return Unauthorized("Invalid Attempt!");

                PatientRefreshTokens obj = new()
                {
                    RefreshToken = token.TokenStr,
                    Email = patient.Email
                };
                PatientService.AddUserRefreshTokens(obj);
                PatientService.SaveCommit();
                return Ok(token);
            }
            return BadRequest("Email Already Exist");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (id != patient.Id)
                    return BadRequest();

                await repository.UpdateAsync(id, patient);
                return Ok(patient);

            }
            return NotFound();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<Patient>> Login(PatientLoginDTO p)
        {
            if (!String.IsNullOrEmpty(p.Email) && !String.IsNullOrEmpty(p.Password))
            {
                if (PatientService.IsValidUser(p.Email, p.Password))
                {
                    var patient = await GetPatient(p.Email);
                    if (patient == null)
                    {
                        return BadRequest();
                    }

                    Token? token = JWTManager.GenerateToken(patient);
                    if (token != null)
                    {
                        PatientRefreshTokens Prt = new()
                        {
                            RefreshToken = token.RefreshToken,
                            Email = patient.Email
                        };
                        PatientService.AddUserRefreshTokens(Prt);
                        PatientService.SaveCommit();
                        return Ok(token);
                    }
                    else Unauthorized("Invalid Attempt!");
                }
                return Unauthorized("Incorrect username or password!");
            }
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(Token token)
        {
            var principal = JWTManager.GetPrincipalFromExpiredToken(token.TokenStr);
            var Email = principal.Claims.ElementAt(1).Value;
            var patient = await GetPatient(Email);

            //retrieve the saved refresh token from database
            var savedRefreshToken = PatientService.GetSavedRefreshTokens(Email, token.RefreshToken);

            if (savedRefreshToken == null || savedRefreshToken.RefreshToken != token.RefreshToken)
                return Unauthorized("Invalid attempt!");

            var newJwtToken = JWTManager.GenerateRefreshToken(patient);

            if (newJwtToken == null)
                return Unauthorized("Invalid attempt!");

            // saving refresh token to the db
            PatientRefreshTokens obj = new()
            {
                RefreshToken = newJwtToken.RefreshToken,
                Email = Email
            };

            PatientService.DeleteUserRefreshTokens(Email, token.RefreshToken);
            PatientService.AddUserRefreshTokens(obj);
            PatientService.SaveCommit();

            return Ok(newJwtToken);
        }

        private async Task<bool> IsExists(string email)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                return patients.Any(p => p.Email == email);
            }
            return false;
        }

        private async Task<Patient?> GetPatient(string email)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                return patients.FirstOrDefault(p => p.Email == email);
            }
            return null;
        }
    }
}
