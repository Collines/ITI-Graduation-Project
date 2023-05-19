using GraduationProject_DAL.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject_DAL.Handlers
{
    public static class TokenGenerator
    {

        private static readonly int ExpireTimeInHrs = 2;

        public static string GenerateAccessToken(Patient patient, string secretKey)
        {
            var claims = new[]
            {
            new Claim("UserId", patient.Id.ToString()),
            new Claim("Name", $"{patient.FirstName} {patient.LastName}"),
            new Claim("Email", patient.Email)
            // Add additional claims as needed
            };

            var key = Encoding.UTF8.GetBytes(secretKey);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(ExpireTimeInHrs), // Set the access token expiration time
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
