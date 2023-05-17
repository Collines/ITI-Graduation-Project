using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class ReservationRepo : IReservationRepo//:GeneralRepo<Department>
    {
        private readonly HospitalBDContext context;

        public ReservationRepo(HospitalBDContext _context)
        {
            context = _context;            
        }
        public async Task<List<Reservation>> GetAllReservation()
        {
            return await context.Reservations.Include(r => r.Doctor)
                                       .Include(p=>p.Patient)
                                       .ToListAsync();
        }

        public async Task<Reservation?> GetReservationDetails(int id)
        {
             return await context.Reservations.Include(r => r.Doctor)
                                        .Include(p => p.Patient)
                                        .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async void InsertReservation(Reservation reservation)
        {
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();
        }

        public async void UpdateReservation(int id, Reservation reservation)
        {
            Reservation? oldReservation = await context.Reservations.FindAsync(id);
            if (oldReservation != null)
            {
                oldReservation.DateTime = reservation.DateTime;
                oldReservation.Queue = reservation.Queue;
                oldReservation.PId = reservation.PId;
                oldReservation.DocId = reservation.DocId;
                await context.SaveChangesAsync();
            }
        }
        public async void DeleteReservation(int id)
        {
            var reservation = await context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                context.Remove(reservation);
                await context.SaveChangesAsync();
            }
        }
    }
}
