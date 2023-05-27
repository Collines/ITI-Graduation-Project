using GraduationProject_BL.DTO.CampImageDTOs;
using GraduationProject_BL.DTO.DepartmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Interfaces
{
    public interface ICampImageManager
    {
        public Task<List<CampImageDTO>> GetAllAsync();
        public Task<CampImageDTO?> GetByIdAsync(int id);
        public Task InsertAsync(CampImageInsertDTO item);
        public Task DeleteAsync(int id);
    }
}
