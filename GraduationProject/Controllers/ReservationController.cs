using GraduationProject_BL.DTO;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReservationManager manager;

        public ReservationController(IReservationManager _manager, IHttpContextAccessor _httpContextAccessor)
        {
            manager = _manager;
            httpContextAccessor = _httpContextAccessor;
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
