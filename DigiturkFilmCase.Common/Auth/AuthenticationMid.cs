using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DigiturkFilmCase.Common.Auth
{
    public class AuthenticationMid
    {
        //Authentication ve Authorization işlemleri için oluşturuldu.
        private readonly RequestDelegate _nextMiddleWare;
        public AuthenticationMid(RequestDelegate next)
        {
            _nextMiddleWare = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            try
            {
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring(6).Trim();
                    var credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                    var credentials = credentialString.Split(':');
                    if (credentials[0] == "tansu" && credentials[1] == "123")
                    {
                        var claims = new[]
                        {
                            new Claim("name", credentials[0]),
                            new Claim(ClaimTypes.Role, "1")
                        };
                        var identity = new ClaimsIdentity(claims, "Basic");
                        context.User = new ClaimsPrincipal(identity);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            catch
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            await _nextMiddleWare(context);
        }
    }
}
