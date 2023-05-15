using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GraduationProject_DAL.Handlers;

namespace GraduationProject_DAL.Repositories.Authentication
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
		private readonly IConfiguration iconfiguration;
		private readonly HospitalBDContext context;

		public JWTManagerRepository(IConfiguration iconfiguration, HospitalBDContext context)
		{
			this.iconfiguration = iconfiguration;
			this.context = context;
		}
		public Token? Authenticate(Patient p)
		{
			Patient? patient = context.Patients.FirstOrDefault(pa => p.Email == pa.Email);
			if(patient!=null)
			{
				if (PasswordHandler.VerifyPassword(p.Password, patient.Password, Convert.FromHexString(patient.PasswordSalt)))
				{
					var tokenHandler = new JwtSecurityTokenHandler();
					var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
					var tokenDescriptor = new SecurityTokenDescriptor
					{
						Subject = new ClaimsIdentity(new Claim[]
						{
							new Claim("Id", $"{patient.Id}"),
							new Claim(ClaimTypes.Email, p.Email),
							new Claim(ClaimTypes.Name, patient.FName +" "+ patient.LName),
							new Claim("NameAR", patient.FNameAR +" "+ patient.LNameAR)
						}),
						IssuedAt = DateTime.UtcNow,
						Expires = DateTime.UtcNow.AddMinutes(60),
						SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
						Issuer = iconfiguration["JWT:Issuer"],
						Audience = iconfiguration["JWT:Audience"],
					};
					var token = tokenHandler.CreateToken(tokenDescriptor);
					return new Token { TokenStr = tokenHandler.WriteToken(token) };
				}
				else return null;
			}
			return null;
		}
	}
}
