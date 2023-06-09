﻿using GraduationProject_BL.Interfaces;
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
            var headers = context.Request.Headers;
            if (headers != null)
            {
                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();

                if (!string.IsNullOrEmpty(authorizationHeader))
                {
                    var parts = authorizationHeader.Split(' ');
                    if (parts.Length == 2 && parts[0].Equals("Bearer", StringComparison.OrdinalIgnoreCase))
                    {
                        var token = parts[1];

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
                                        ValidateLifetime = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(key)
                                    }, out var validatedToken);

                                    if (principal != null && validatedToken != null)
                                    {
                                        var adminClaim = principal.FindFirst("AdminId");
                                        var userClaim = principal.FindFirst("UserId");

                                        if (adminClaim != null || userClaim != null)
                                        {
                                            if (userClaim != null)
                                            {
                                                // Retrieve user login details from the UserLogins table
                                                var userId = userClaim.Value;
                                                var isFound = await _manager.FindUser(userId);

                                                if (isFound)
                                                {
                                                    // Perform additional authorization checks or set the user's identity
                                                    var identity = new ClaimsIdentity(principal.Identity);
                                                    // Add any additional claims to the identity as needed
                                                    context.User = new ClaimsPrincipal(identity);
                                                }
                                                else
                                                {
                                                    context.Response.StatusCode = 401;
                                                    return;
                                                }
                                            }
                                            else if (adminClaim != null)
                                            {
                                                var adminId = adminClaim.Value;
                                                // TODO: Add Admins Logins
                                                if (adminId == null)
                                                {
                                                    context.Response.StatusCode = 401;
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            context.Response.StatusCode = 401;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        context.Response.StatusCode = 401;
                                        return;
                                    }
                                }
                                catch
                                {
                                    // Token validation failed
                                    context.Response.StatusCode = 401;
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
