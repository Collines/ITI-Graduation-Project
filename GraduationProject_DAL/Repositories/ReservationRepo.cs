using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class ReservationRepo : IReservationRepo
    {
        private readonly HospitalBDContext context;

        public ReservationRepo(HospitalBDContext _context)
        {
            context = _context;            
        }
        public List<Reservation> GetAllReservation()
        {
            return context.Reservations.Include(r => r.Doctor)
                                       .Include(p=>p.Patient)
                                       .ToList();
        }

        public Reservation? GetReservationDetails(int id)
        {
             return context.Reservations.Include(r => r.Doctor)
                                        .Include(p => p.Patient)
                                        .FirstOrDefault(x=>x.Id == id);
        }

        public void InsertReservation(Reservation reservation)
        {
            context.Reservations.Add(reservation);
            context.SaveChanges();
        }

        public void UpdateReservation(int id, Reservation reservation)
        {
            Reservation? oldReservation = context.Reservations.Find(id);
            if (oldReservation != null)
            {
                oldReservation.DateTime = reservation.DateTime;
                oldReservation.Queue = reservation.Queue;
                oldReservation.PId = reservation.PId;
                oldReservation.DocId = reservation.DocId;
                context.SaveChanges();
            }
        }
        public void DeleteReservation(int id)
        {
            var reservation = context.Reservations.Find(id);
            if (reservation != null)
            {
                context.Remove(reservation);
                context.SaveChanges();
            }
        }
    }
}
