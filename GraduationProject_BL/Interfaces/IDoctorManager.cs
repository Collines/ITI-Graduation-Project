using GraduationProject_BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Interfaces
{
    public interface IDoctorManager
    {
        public Task<List<DoctorDTO>> GetAllAsync(string lang);
        public Task<DoctorInsertDTO?> GetInsertDTOByIdAsync(int id);
        public Task<DoctorDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(DoctorInsertDTO item);
        public Task UpdateAsync(int id, DoctorInsertDTO item);
        public Task DeleteAsync(int id);
    }
}
