using shopping_list_api.Models;
using shopping_list_api.ServiceModels;

namespace shopping_list_api.Services
{
    public interface IAuthenticationService
    {
        Task<UserPermissionViewModel?> Login(string username, string password);
        Task<UserPermissionViewModel?> GetMe();
        Task<bool> AddUserToken(int userId, string token);
        bool ValidateUserToken(int userId, string token);
        Task<bool> InValidateUserToken();
        List<int> GetUserPermission();
    }
} 