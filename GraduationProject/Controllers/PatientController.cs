using GraduationProject_BL.DTO;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPatientManager manager;
        private readonly ITranslations<PatientTranslations> translations;

        public PatientController(IHttpContextAccessor _httpContextAccessor, IPatientManager _manager, ITranslations<PatientTranslations> translations)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
            this.translations = translations;
        }

        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetAll()
        {
            var patients = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));
            if (patients.Count == 0)
            {
                return NotFound();
            }
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientDetails(int id)
        {
            var patient = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));

            if (patient != null)
            {
                return Ok(patient);
            }

            return NotFound();
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDTO>> Register(PatientInsertDTO patient)
        {
            if (await manager.FindPatient(patient.Email))
                ModelState.AddModelError("Email", "Email Already Exist");

            if (ModelState.IsValid)
            {
                var dto = await manager.InsertAsync(patient);

                if (dto != null)
                {
                    return Ok(dto);
                }
                else
                {
                    return Unauthorized("Invalid Attempt!");
                }
            }
            return BadRequest("Email Already Exist");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [HttpPatch("Update")]
        public async Task<ActionResult<Patient>> UpdatePatient(string accessToken, PatientUpdateDTO patientUDTO)
        {
             var patient = await manager.GetPatientByAccessToken(accessToken);
            if (patient != null)
            {
                if (patientUDTO.Email != patient.Email)
                {
                    if (await manager.FindPatient(patientUDTO.Email))
                    {
                        ModelState.AddModelError("Email", "Email Already Exist");
                        return BadRequest("Email Already Exist");
                    }
                    await manager.UpdateAsync(patient.Id, patientUDTO);
                    return Ok(new {message="Edited Successfully"});
                } else
                {
                    await manager.UpdateAsync(patient.Id, patientUDTO);
                    return Ok(new { message = "Edited Successfully" });
                }
            }
            return BadRequest();
        }

        [HttpPost("GetPatientData")]

        public async Task<ActionResult<PatientEditDTO>> GetPatientDetails([FromBody]string accessToken)
        {
            if (ModelState.IsValid)
            {
                var patient = await manager.GetPatientByAccessToken(accessToken);
                
                if (patient != null)
                {
                    var translation = await translations.FindAsync(patient.Id);
                    var patientUDTO = new PatientEditDTO()
                    {
                        SSN = patient.SSN,
                        Gender = patient.Gender,
                        FirstName_EN = patient.FirstName,
                        LastName_EN = patient.LastName,
                        FirstName_AR = translation?.FirstName_AR ?? "",
                        LastName_AR = translation?.LastName_AR ?? "",
                        Email = patient.Email,
                        Phone = patient.PhoneNumber,
                        MedicalHistory = patient.MedicalHistory,
                        DOB = patient.DOB,
                    };
                    return Ok(patientUDTO);
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginDTO>> Login(PatientLoginDTO login)
        {
            if (!string.IsNullOrEmpty(login.Email) && !string.IsNullOrEmpty(login.Password))
            {

                var isFound = await manager.FindPatient(login.Email, login.Password);

                if (isFound)
                {
                    var dto = await manager.Login(login.Email);

                    if (dto != null)
                    {
                        return Ok(dto);
                    }
                    else
                    {
                        return Unauthorized("Invalid Attempt!");
                    }
                }
                return Unauthorized("Incorrect username or password!");
            }
            else return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDTO token)
        {
            if (token.RefreshToken == null)
            {
                return BadRequest();
            }

            var isFound = await manager.FindPatientByRefreshToken(token.RefreshToken);

            if (isFound !=null)
            {
                var dto = await manager.Refresh(token.RefreshToken);

                if (dto != null)
                {
                    return Ok(dto);
                }
                else
                {
                    return Unauthorized("Invalid Attempt!");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
