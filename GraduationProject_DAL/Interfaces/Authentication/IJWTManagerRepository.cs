using GraduationProject_DAL.Data.Models;
using System.Security.Claims;


namespace GraduationProject_DAL.Interfaces.Authentication
{
	public interface IJWTManagerRepository
	{
		Token? GenerateToken(Patient patient);
		Token? GenerateRefreshToken(Patient? patient);
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
