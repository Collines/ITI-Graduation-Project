using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject
{
    internal static class Utils
    {
        internal static string GetLang(IHttpContextAccessor httpContextAccessor)
        {
            string lang = "en";

            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var headers = httpContext.Request.Headers;
                if (headers.ContainsKey("Lang"))
                {
                    var headerValue = headers["Lang"];
                    lang = headerValue.ToString();
                }
            }

            return lang;
        }
    }
}
