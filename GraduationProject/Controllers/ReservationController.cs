using GraduationProject_BL.DTO.ReservationDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPatientManager patientMgr;
        private readonly IDoctorManager doctorMgr;
        private readonly IReservationManager manager;

        public ReservationController(IReservationManager _manager, IHttpContextAccessor _httpContextAccessor, IPatientManager patientMgr, IDoctorManager doctormgr)
        {
            manager = _manager;
            httpContextAccessor = _httpContextAccessor;
            this.patientMgr = patientMgr;
            doctorMgr = doctormgr;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationDTO>>> GetAll()
        {
            var reservations = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservationDTO>> GetById(int id)
        {
            var reservation = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));

            if (reservation != null)
            {
                return Ok(reservation);
            }

            return NotFound();
        }

        [HttpPost("My-Reservations")]
        public async Task<ActionResult<List<ReservationDTO>>> GetAllPatientReservations(int patientId, [FromBody] string accessToken)
        {
            var patient = await patientMgr.GetPatientByAccessToken(accessToken);
            if (patient != null)
            {
                if (patientId != patient.Id)
                    return Unauthorized();
                var reservations = await manager.GetAllPatientReservationsAsync(Utils.GetLang(httpContextAccessor), patientId);
                if (reservations != null && reservations.Count > 0)
                    return Ok(reservations);

                return NotFound();
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation, string accessToken)
        {
            var patient = await patientMgr.GetPatientByAccessToken(accessToken);
            var doctor = await doctorMgr.GetByIdAsync(reservation.DoctorId, Utils.GetLang(httpContextAccessor));
            if (patient != null && doctor != null && reservation.PatientId == patient.Id)
            {
                if(await manager.InsertAsync(reservation))
                    return Ok();
                return BadRequest("This day is fully occupied");
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            await manager.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            await manager.UpdateAsync(id, reservation);
            return Ok(reservation);
        }

        [HttpPatch("CancelReservation/{id:int}")]
        public async Task<ActionResult<ReservationDTO>> CancelReservation(int id, ReservationDTO reservation)
        {
            if (id != reservation.Id || User.Claims.FirstOrDefault().Value != reservation.PatientId.ToString())
                return BadRequest();
            await manager.CancelReservation(id);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Count")]
        public async Task<IActionResult> GetReservationsCount()
        {
            var reservations = await manager.GetAllAsync(Utils.GetLang(httpContextAccessor));

            return Ok(reservations.Count);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("ReservationStatus/{id:int}")]
        public async Task<ActionResult<Reservation>> ChangeReservationStatus(int id, [FromForm] ReservationStatus status)
        {
            var reservation = await manager.GetByIdAsync(id, Utils.GetLang(httpContextAccessor));
            if (reservation != null)
            {
                await manager.ChangeReservationStatus(id, status);
                return Ok();
            }
            return BadRequest();
        }
    }
}
