using GraduationProject_BL.DTO.DoctorDTOs;

namespace GraduationProject_BL.Interfaces
{
    public interface IDoctorManager
    {
        public Task<List<DoctorDTO>> GetAllAsync(string lang);
        public Task<List<DoctorDTO>?> GetDepartmentDoctors(int deptId, string lang);
        public Task<DoctorInsertDTO?> GetInsertDTOByIdAsync(int id);
        public Task<DoctorDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(DoctorFormData formData);
        public Task UpdateAsync(int id, DoctorFormData formData);
        public Task DeleteAsync(int id);
        public Task<DoctorInsertDTO> DoctorFormDataToDoctorInsertDTO(DoctorFormData item);
    }
}
