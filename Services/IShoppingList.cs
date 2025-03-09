using shopping_list_api.Models;
using shopping_list_api.ServiceModels;

namespace shopping_list_api.Services
{
    public interface IShoppingList
    {
        Task<Tuple<List<ShoppingList>, int>> GetShoppingLists(DataTableJSONViewModel reqBody);
        Task<ShoppingList?> GetShoppingList(int id);
        Task<ShoppingList> AddShoppingList(ShoppingList model);
        Task<ShoppingList?> UpdateShoppingList(ShoppingList model);
        Task<bool> DeleteShoppingList(int id);
        Task<ShoppingListItem?> AddShoppingListItem(ShoppingListItem model);
        Task<ShoppingListItem?> UpdateShoppingListItem(ShoppingListItem model);
        Task<bool> DeleteShoppingListItem(int id);
        Task<bool> ToggleItemCompletion(int id);
    }
} 