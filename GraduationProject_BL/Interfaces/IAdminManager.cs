using GraduationProject_BL.DTO.Admin;

namespace GraduationProject_BL.Interfaces
{
    public interface IAdminManager
    {
        public Task<bool> FindAdmin(string username, string password);

        public Task<AdminDTO?> Login(string username);
    }
}
