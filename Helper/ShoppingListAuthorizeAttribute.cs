using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shopping_list_api.Enumerations;
using shopping_list_api.Services;

namespace shopping_list_api.Helper
{
    public class ShoppingListAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly Permissions _permission;

        public ShoppingListAuthorizeAttribute(Permissions permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticationService = context.HttpContext.RequestServices.GetService<IAuthenticationService>();
            if (authenticationService != null)
            {
                var userPermissions = authenticationService.GetUserPermission();
                if (!userPermissions.Contains((int)_permission))
                {
                    context.Result = new UnauthorizedResult();
                }
            }
        }
    }
} 