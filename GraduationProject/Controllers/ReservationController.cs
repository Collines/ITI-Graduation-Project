using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using GraduationProject_DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepo ReservationRepo;

        public ReservationController(IReservationRepo _repo)
        {
            ReservationRepo = _repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAll()
        {
            var reservations = await ReservationRepo.GetAllReservation();

            if (reservations.Count == 0)
                return NotFound();

            return Ok(reservations);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reservation>> GetById(int id)
        {
            var reservation = await ReservationRepo.GetReservationDetails(id);

            if (reservation == null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult<Reservation> CreateReservation(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                ReservationRepo.InsertReservation(reservation);
                return Ok(reservation);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(int id)
        {
            var reservation =await ReservationRepo.GetReservationDetails(id);

            if (reservation == null)
                return NotFound();

            ReservationRepo.DeleteReservation(id);
            return Ok(reservation);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Reservation>> UpdateReservation(int id, Reservation reservation)
        {
            var updatedreservation = await ReservationRepo.GetReservationDetails(id);

            if (updatedreservation == null)
                return NotFound();
            ReservationRepo.UpdateReservation(id, reservation);

            return Ok(reservation);
        }
    }
}
