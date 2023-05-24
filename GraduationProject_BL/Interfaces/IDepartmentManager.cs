using GraduationProject_BL.DTO.DepartmentDTOs;

namespace GraduationProject_BL.Managers
{
    public interface IDepartmentManager
    {
        public Task<List<DepartmentDTO>> GetAllAsync(string lang);
        public Task<DepartmentDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(DepartmentInsertDTO item);
        public Task UpdateAsync(int id, DepartmentInsertDTO item);
        public Task DeleteAsync(int id);
    }
}
