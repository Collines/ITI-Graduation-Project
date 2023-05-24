using GraduationProject_BL.DTO;
using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Interfaces
{
    public interface IReservationManager
    {
        public Task<List<ReservationDTO>> GetAllAsync(string lang);
        public Task<ReservationDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(Reservation item);
        public Task UpdateAsync(int id, Reservation item);
        public Task DeleteAsync(int id);
    }
}
