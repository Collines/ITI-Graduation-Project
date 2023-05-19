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
        public Task<DoctorDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(InsertDoctorDTO item);
        public Task UpdateAsync(int id, InsertDoctorDTO item);
        public Task DeleteAsync(int id);
    }
}
