using GraduationProject_BL.DTO;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPatientManager manager;


        public PatientController(IHttpContextAccessor _httpContextAccessor, IPatientManager _manager)
        {
            httpContextAccessor = _httpContextAccessor;
            manager = _manager;
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

        [HttpPatch("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, PatientInsertDTO patient)
        {
            if (ModelState.IsValid)
            {
                await manager.UpdateAsync(id, patient);
                return Ok(patient);

            }
            return NotFound();
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

            if (isFound)
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
