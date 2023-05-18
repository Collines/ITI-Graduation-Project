using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject_BL.Managers
{
    public class PatientLoginManager : IPatientLoginManager
    {
        private readonly IRepository<PatientsLogins> repository;

        public PatientLoginManager(IRepository<PatientsLogins> _repository)
        {
            repository = _repository;
        }

        public async Task<bool> FindUser(string? userId)
        {
            if (userId != null)
            {
                var logins = await repository.GetAllAsync();

                if (logins != null)
                {
                    var login = logins.Find(x => x.PatientId.ToString() == userId);
                    if (login != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
