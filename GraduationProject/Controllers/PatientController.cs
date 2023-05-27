using GraduationProject_BL.DTO;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPatientManager manager;

        public PatientController(IHttpContextAccessor _httpContextAccessor, IPatientManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> GetAll()
        {
            var patients = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(patients);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            await manager.DeleteAsync(id);
            return Ok(id);
        }

        [Authorize(Roles = "Patient")]
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
                    return Ok(new { message = "Edited Successfully" });
                }
                else
                {
                    await manager.UpdateAsync(patient.Id, patientUDTO);
                    return Ok(new { message = "Edited Successfully" });
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("GetPatientData")]
        public async Task<ActionResult<PatientEditDTO>> GetPatientDetails([FromBody] string accessToken)
        {
            if (ModelState.IsValid)
            {
                var patientUDTO = await manager.GetPatientEditDTOByAccessToken(accessToken);
                if (patientUDTO != null)
                    return Ok(patientUDTO);
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
                        return Unauthorized(new { ResponseMessage = "Invalid Attempt!" });
                    }
                }
                return Unauthorized(new {ResponseMessage= "Incorrect username or password!" });
            }
            else return BadRequest(new { ResponseMessage = "Incorrect username or password!" });
        }

        [Authorize]
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenDTO token)
        {
            if (token.RefreshToken == null)
            {
                return BadRequest();
            }

            var isFound = await manager.FindPatientByRefreshToken(token.RefreshToken);

            if (isFound != null)
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
