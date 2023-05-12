using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces
{
    public interface IReservationRepo
    {
        public List<Reservation> GetAllReservation();
        public Reservation GetReservationDetails(int id);
        public void InsertReservation(Reservation reservation);
        public void UpdateReservation(int id, Reservation reservation);
        public void DeleteReservation(int id);
    }
}
