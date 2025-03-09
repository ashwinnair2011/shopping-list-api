using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shopping_list_api.Helper;
using shopping_list_api.Models;
using shopping_list_api.ServiceModels;
using shopping_list_api.Services;

namespace shopping_list_api.Controllers
{
    [Route("api/shopping-list")]
    [ApiController]
    [Authorize]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingList _shoppingList;
        private readonly ILogger<ShoppingListController> _logger;

        public ShoppingListController(IShoppingList shoppingList, ILogger<ShoppingListController> logger)
        {
            _shoppingList = shoppingList;
            _logger = logger;
        }

        [HttpPost("")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canViewShoppingLists)]
        public async Task<IActionResult> getShoppingLists(DataTableJSONViewModel reqBody)
        {
            try
            {
                var result = await _shoppingList.GetShoppingLists(reqBody);
                return Ok(new { success = true, result = result.Item1, filteredRecords = result.Item1.Count, totalRecords = result.Item2 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpGet("")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canViewShoppingLists)]
        public async Task<IActionResult> getShoppingList(int id)
        {
            try
            {
                var result = await _shoppingList.GetShoppingList(id);
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPost("create")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> addShoppingList(ShoppingList model)
        {
            try
            {
                var result = await _shoppingList.AddShoppingList(model);
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPut("")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> updateShoppingList(ShoppingList model)
        {
            try
            {
                var result = await _shoppingList.UpdateShoppingList(model);
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpDelete("")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> deleteShoppingList(int id)
        {
            try
            {
                var result = await _shoppingList.DeleteShoppingList(id);
                if (result)
                    return Ok(new { success = true, result = "Shopping list deleted" });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPost("item")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> addShoppingListItem(ShoppingListItem model)
        {
            try
            {
                var result = await _shoppingList.AddShoppingListItem(model);
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list item information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPut("item")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> updateShoppingListItem(ShoppingListItem model)
        {
            try
            {
                var result = await _shoppingList.UpdateShoppingListItem(model);
                if (result != null)
                    return Ok(new { success = true, result = result });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list item information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpDelete("item")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> deleteShoppingListItem(int id)
        {
            try
            {
                var result = await _shoppingList.DeleteShoppingListItem(id);
                if (result)
                    return Ok(new { success = true, result = "Shopping list item deleted" });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list item information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPut("item/toggle")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifyShoppingLists)]
        public async Task<IActionResult> toggleItemCompletion(int id)
        {
            try
            {
                var result = await _shoppingList.ToggleItemCompletion(id);
                if (result)
                    return Ok(new { success = true, result = "Item completion toggled" });
                else
                    return NotFound(new { success = false, result = "Invalid shopping list item information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }
    }
} 