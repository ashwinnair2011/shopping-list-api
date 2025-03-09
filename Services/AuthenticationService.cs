using Microsoft.EntityFrameworkCore;
using shopping_list_api.Models;
using shopping_list_api.ServiceModels;
using System.Security.Claims;

namespace shopping_list_api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ShoppingListDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, ShoppingListDbContext dbContext, ILogger<AuthenticationService> logger)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<UserPermissionViewModel?> Login(string username, string password)
        {
            try
            {
                var userRecord = await _dbContext.Users
                    .FirstOrDefaultAsync(user => user.UserName.ToUpper() == username.ToUpper());

                if (userRecord != null && BCrypt.Net.BCrypt.Verify(password, userRecord.UserPassword))
                {
                    var permissions = await (from up in _dbContext.UserPermissions
                                          join per in _dbContext.Permissions on up.PermissionId equals per.PermissionId
                                          where up.UserId == userRecord.UserId
                                          select per.PermissionName).ToListAsync();

                    return new UserPermissionViewModel
                    {
                        UserId = userRecord.UserId,
                        UserName = userRecord.UserName,
                        Permissions = permissions,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {Username}", username);
                throw;
            }
        }

        public async Task<UserPermissionViewModel?> GetMe()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available");
                var userId = Convert.ToInt32(context.User.FindFirstValue("UserId"));
                var userRecord = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (userRecord != null)
                {
                    var permissions = await (from up in _dbContext.UserPermissions
                                          join per in _dbContext.Permissions on up.PermissionId equals per.PermissionId
                                          where up.UserId == userRecord.UserId
                                          select per.PermissionName).ToListAsync();

                    return new UserPermissionViewModel
                    {
                        UserId = userRecord.UserId,
                        UserName = userRecord.UserName,
                        Permissions = permissions,
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current user details");
                throw;
            }
        }

        public async Task<bool> AddUserToken(int userId, string token)
        {
            try
            {
                var record = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                if (record != null)
                {
                    record.UserToken = token;
                    _dbContext.Users.Update(record);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user token for user {UserId}", userId);
                throw;
            }
        }

        public bool ValidateUserToken(int userId, string token)
        {
            try
            {
                var record = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
                return record != null && record.UserToken == token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating user token for user {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> InValidateUserToken()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available");
                var userId = Convert.ToInt32(context.User.FindFirstValue("UserId"));
                var record = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                
                if (record != null)
                {
                    record.UserToken = "";
                    _dbContext.Users.Update(record);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error invalidating user token");
                throw;
            }
        }

        public List<int> GetUserPermission()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available");
                var userId = Convert.ToInt32(context.User.FindFirstValue("UserId"));
                return _dbContext.UserPermissions
                    .Where(x => x.UserId == userId)
                    .Select(x => x.PermissionId)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user permissions");
                throw;
            }
        }
    }
} 