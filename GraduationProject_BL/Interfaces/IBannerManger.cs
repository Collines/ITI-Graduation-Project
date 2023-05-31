using GraduationProject_BL.DTO.BannerDTOs;


namespace GraduationProject_BL.Interfaces
{
    public interface IBannerManger
    {
        public Task<List<BannerDTO>> GetAllAsync(string lang);

        public Task<BannerDTO> GetByIdAsync(int id, string lang);

        public Task InsertAsync(BannerFormData item);
        public Task UpdateAsync(int id, BannerFormData item);
        public Task DeleteAsync(int id);

        public Task<BannerInsertDTO?> GetInsertDTOByIdAsync(int id);
    }
}
