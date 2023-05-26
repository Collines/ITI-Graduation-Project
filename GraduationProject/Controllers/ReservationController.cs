using GraduationProject_BL.DTO.ReservationDTOs;
using GraduationProject_BL.Interfaces;
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
        private readonly IReservationManager manager;

        public ReservationController(IReservationManager _manager, IHttpContextAccessor _httpContextAccessor, IPatientManager patientMgr)
        {
            manager = _manager;
            httpContextAccessor = _httpContextAccessor;
            this.patientMgr = patientMgr;
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
        public async Task<ActionResult<List<PatientReservationDTO>>> GetAllPatientReservations(int patientId,[FromBody]string accessToken)
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
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await manager.InsertAsync(reservation);
                return Ok();
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
    }
}
