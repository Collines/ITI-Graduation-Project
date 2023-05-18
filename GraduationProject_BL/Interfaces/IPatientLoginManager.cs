namespace GraduationProject_BL.Interfaces
{
    public interface IPatientLoginManager
    {
        public Task<bool> FindUser(string? userId);
    }
}
