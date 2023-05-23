using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository<Reservation> repository;

        public ReservationController(IRepository<Reservation> _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAll()
        {
            var reservations = await repository.GetAllAsync();

            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reservation>> GetById(int id)
        {
            var reservations = await repository.GetAllAsync();

            if (reservations != null)
            {
                var reservation = reservations.Find(x => x.Id == id);
                if (reservation != null)
                {
                    return Ok(reservation);
                }
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await repository.InsertAsync(reservation);
                return Ok(reservation);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            await repository.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            await repository.UpdateAsync(id, reservation);
            return Ok(reservation);
        }
    }
}
