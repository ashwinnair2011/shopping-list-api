using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using shopping_list_api.Services;
using System.IdentityModel.Tokens.Jwt;

namespace shopping_list_api.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IAuthenticationService authenticationService)
        {
            try
            {
                var endpoint = httpContext.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                {
                    await _next(httpContext);
                    return;
                }

                string? authorization = httpContext.Request.Headers.Authorization;
                if (string.IsNullOrEmpty(authorization) ||
                !authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    httpContext.Response.StatusCode = 401;
                    await HandleInvalidToken(httpContext);
                    return;
                }

                var token = authorization.Substring("Bearer ".Length).Trim();
                var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var userId = jwtSecurityToken?.Claims?.SingleOrDefault(p => p.Type == "UserId")?.Value;

                if (userId != null && authenticationService.ValidateUserToken(int.Parse(userId), authorization))
                {
                    // Set user context
                    httpContext.Items["UserId"] = userId;
                    httpContext.Items["UserName"] = jwtSecurityToken?.Claims?.SingleOrDefault(p => p.Type == "UserName")?.Value;
                    httpContext.Items["Permissions"] = jwtSecurityToken?.Claims?.SingleOrDefault(p => p.Type == "Permissions")?.Value;

                    await _next(httpContext);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "  " + ex.StackTrace);
            }

            await HandleInvalidToken(httpContext);
        }

        private static Task HandleInvalidToken(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            httpContext.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(new { success = false, result = "Missing or invalid token" });
            return httpContext.Response.WriteAsync(json);
        }
    }

    public static class JWTMiddlewareExtensions
    {
        public static IApplicationBuilder UseJWTMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
} 