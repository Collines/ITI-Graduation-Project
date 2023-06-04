using GraduationProject_DAL.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject_DAL.Handlers
{
    public static class TokenGenerator
    {

        private static readonly int ExpireTimeInDays = 7;

        public static string GenerateAccessToken(Patient patient, string secretKey)
        {
            var claims = new[]
            {
            new Claim("UserId", patient.Id.ToString()),
            new Claim("Name", $"{patient.FirstName} {patient.LastName}"),
            new Claim("Email", patient.Email),
            new Claim(ClaimTypes.Role, "Patient")
            // Add additional claims as needed
            };

            return GenerateToken(claims, secretKey);
        }

        public static string GenerateAdminAccessToken(Admin admin, string secretKey)
        {
            var claims = new[]
            {
            new Claim("AdminId", admin.Id.ToString()),
            new Claim("AdminUserName", admin.UserName),
            new Claim(ClaimTypes.Role, "Admin")
            // Add additional claims as needed
            };

            return GenerateToken(claims, secretKey);
        }

        private static string GenerateToken(Claim[] claims, string secretKey)
        {
            var key = Encoding.UTF8.GetBytes(secretKey);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(ExpireTimeInDays), // Set the access token expiration time
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            // Generate a unique refresh token
            var refreshToken = Guid.NewGuid().ToString();
            return refreshToken;
        }

        public static long GetExpirationTime(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            if (token.Payload.TryGetValue("exp", out var expValue))
            {
                if (expValue is long expUnixTime)
                {
                    return expUnixTime;
                }
            }

            return -1;
        }
    }
}
