using GraduationProject_BL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject.Middlewares
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration _iConfiguration, IPatientLoginManager _manager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _iConfiguration["JWT:Key"];
                if (secretKey != null)
                {
                    var key = Encoding.UTF8.GetBytes(secretKey);

                    try
                    {
                        // Validate the token
                        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _iConfiguration["JWT:Issuer"],
                            ValidAudience = _iConfiguration["JWT:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(key)
                        }, out var validatedToken);

                        if (principal != null && validatedToken != null)
                        {
                            var claim = principal.FindFirst("UserId");
                            if (claim != null)
                            {
                                // Retrieve user login details from the UserLogins table
                                var userId = claim.Value;
                                var isFound = await _manager.FindUser(userId);

                                if (isFound)
                                {
                                    // Perform additional authorization checks or set the user's identity
                                    var identity = new ClaimsIdentity(principal.Identity);
                                    // Add any additional claims to the identity as needed
                                    context.User = new ClaimsPrincipal(identity);
                                }
                            }
                        }
                    }
                    catch (SecurityTokenValidationException)
                    {
                        // Token validation failed
                        context.Response.StatusCode = 401;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
