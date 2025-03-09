using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using shopping_list_api.Services;

namespace shopping_list_api.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authentication;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authentication, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _authentication = authentication;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                var result = await _authentication.Login(username, password);
                if (result == null)
                {
                    return NotFound(new { success = false, result = "Invalid user credentials" });
                }
                else
                {
                    if (result.Permissions.Contains(Enumerations.Permissions.canLogInToApi.ToString()))
                    {
                        var claims = new List<Claim>
                        {
                            new("UserId", result.UserId.ToString()),
                        };

                        var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured");
                        var jwtIssuer = _configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured");
                        var jwtAudience = _configuration["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured");

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            jwtIssuer,
                            jwtAudience,
                            claims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: signIn);

                        var userToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
                        await _authentication.AddUserToken(result.UserId, userToken);

                        return Ok(new { success = true, token = userToken });
                    }
                    else
                        return Unauthorized(new { success = false, result = "User not authenticated to log in for API usage" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login attempt");
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpGet("me")]
        public async Task<IActionResult> me()
        {
            try
            {
                var result = await _authentication.GetMe();
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid user details" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user details");
                return Unauthorized(new { success = false, result = ex.Message });
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _authentication.InValidateUserToken();
                return Ok(new { success = true, result = "Logged out" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return Unauthorized(new { success = false, result = ex.Message });
            }
        }
    }
} 