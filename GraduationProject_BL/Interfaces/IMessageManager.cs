using GraduationProject_BL.DTO.ReservationDTOs;
using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Interfaces
{
    public interface IMessageManager
    {
        public Task<List<Message>> GetAllAsync();

        public Task DeleteAsync(int id);

        public Task UpdateAsync(int id, Message item);

        public Task<Message?> GetByIdAsync(int id);

        public Task InsertAsync(Message item);
    }
}
