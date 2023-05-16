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
		private readonly IPatientServiceRepository PatientService;

		public PatientController(IPatientRepo Prepo, IJWTManagerRepository jWTManager, IPatientServiceRepository patientService)
        {
            patientRepo = Prepo;
			JWTManager = jWTManager;
			PatientService = patientService;
		}
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await patientRepo.GetAllPatients();
            if (patients.Count == 0)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientDetails(int id)
        {
            var patients =await patientRepo.GetPatientDetails(id);
            if (patients == null)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpPost("Register")]
		[AllowAnonymous]
		public async Task<ActionResult<Patient>> Register(Patient patient)
        {
            if (await patientRepo.IsExist(patient.Email))
                ModelState.AddModelError("Email", "Email Already Exist");
            if (ModelState.IsValid)
            {
                patient.Password = PasswordHandler.Hash(patient.Password, out byte[] salt);
                patient.PasswordSalt = Convert.ToHexString(salt);
                patientRepo.InsertPatient(patient);
				var token = JWTManager.GenerateToken(patient);
				if (token == null)
					return Unauthorized("Invalid Attempt!");
				PatientRefreshTokens obj = new PatientRefreshTokens
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
            var patient = await patientRepo.GetPatientDetails(id);
            if(patient == null)
            {
                return NotFound();
            }
            patientRepo.DeletePatient(id);
            return Ok(patient);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id,Patient patient)
        {
            if (ModelState.IsValid)
            {
               if(id != patient.Id)
                {
                    return BadRequest();
                }
                var updatePatient = await patientRepo.GetPatientDetails(id);
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
		public async Task<ActionResult<Patient>> Login(PatientLoginDTO p)
		{
            if (!String.IsNullOrEmpty(p.Email) && !String.IsNullOrEmpty(p.Password))
            {
                Patient? patient = new() { Email = p.Email, Password = p.Password };
                if(PatientService.IsValidUser(patient))
                {
                    patient =await patientRepo.GetPatient(p.Email);
                    Token? token = JWTManager.GenerateToken(patient);
					if (token != null)
					{
						PatientRefreshTokens Prt = new PatientRefreshTokens
						{
							RefreshToken = token.RefreshToken,
							Email = patient.Email
						};
						PatientService.AddUserRefreshTokens(Prt);
						PatientService.SaveCommit();
						return Ok(token);
					} else Unauthorized("Invalid Attempt!");
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
            var patient = await patientRepo.GetPatient(Email);

			//retrieve the saved refresh token from database
			var savedRefreshToken = PatientService.GetSavedRefreshTokens(Email, token.RefreshToken);

/*            if (savedRefreshToken != null)
            {
                if (savedRefreshToken.RefreshToken != token.TokenStr)
                {
                    return Unauthorized("Invalid attempt!");
                }
            }
            else return Unauthorized("Invalid attempt!");*/

            if (savedRefreshToken == null || savedRefreshToken.RefreshToken != token.RefreshToken)
				return Unauthorized("Invalid attempt!");

			var newJwtToken = JWTManager.GenerateRefreshToken(patient);

			if (newJwtToken == null)
				return Unauthorized("Invalid attempt!");

			// saving refresh token to the db
			PatientRefreshTokens obj = new PatientRefreshTokens
			{
				RefreshToken = newJwtToken.RefreshToken,
				Email = Email
			};

			PatientService.DeleteUserRefreshTokens(Email, token.RefreshToken);
			PatientService.AddUserRefreshTokens(obj);
			PatientService.SaveCommit();

			return Ok(newJwtToken);
		}
	}
}
