using GraduationProject_BL.DTO.Admin;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace GraduationProject_BL.Managers
{
    public class AdminManager : IAdminManager
    {
        private readonly IConfiguration iConfiguration;
        private readonly IRepository<Admin> repository;

        public AdminManager(
            IConfiguration configuration,
            IRepository<Admin> _repository)
        {
            iConfiguration = configuration;
            repository = _repository;
        }

        public async Task<bool> FindAdmin(string username, string password)
        {
            var admins = await repository.GetAllAsync();
            if (admins != null)
            {
                var admin = admins.Find(a => a.UserName == username && a.Password == password);
                if (admin != null)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<AdminDTO?> Login(string username)
        {
            var admins = await repository.GetAllAsync();
            if (admins != null)
            {
                var admin = admins.Find(p => p.UserName == username);
                if (admin != null)
                {
                    return GetLoginDTO(admin);
                }
            }

            return null;
        }

        private AdminDTO? GetLoginDTO(Admin admin)
        {
            var secretKey = iConfiguration["JWT:Key"];
            if (secretKey != null)
            {
                var accessToken = TokenGenerator.GenerateAdminAccessToken(admin, secretKey);
                var refreshToken = TokenGenerator.GenerateRefreshToken();
                var expiration = TokenGenerator.GetExpirationTime(accessToken);

                AdminDTO dto = new()
                {
                    Id = admin.Id,
                    UserName = admin.UserName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Expiration = expiration
                };

                return dto;
            }

            return null;
        }
    }
}
