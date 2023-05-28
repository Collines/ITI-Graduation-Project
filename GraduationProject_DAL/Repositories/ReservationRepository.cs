using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class ReservationRepository : IRepository<Reservation>
    {
        private readonly HospitalDBContext context;

        public ReservationRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Reservation>> GetAllAsync()
        {
            return await context.Reservations.Include(r => r.Doctor).ThenInclude(d=>d.Department)
                                       .Include(p => p.Patient)
                                       .ToListAsync();
        }

        public async Task InsertAsync(Reservation item)
        {
            await context.Reservations.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Reservation item)
        {
            var reservation = await context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.DateTime = item.DateTime;
                reservation.Queue = item.Queue;
                reservation.PatientId = item.PatientId;
                reservation.DoctorId = item.DoctorId;
                reservation.Status = item.Status;
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
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
