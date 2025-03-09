using Microsoft.EntityFrameworkCore;
using shopping_list_api.Models;
using shopping_list_api.ServiceModels;
using System.Security.Claims;

namespace shopping_list_api.Services
{
    public class ShoppingListService : IShoppingList
    {
        private readonly ShoppingListDbContext _dbContext;
        private readonly ClaimsPrincipal _user;
        private readonly ILogger<ShoppingListService> _logger;

        public ShoppingListService(IHttpContextAccessor httpContextAccessor, ShoppingListDbContext dbContext, ILogger<ShoppingListService> logger)
        {
            _dbContext = dbContext;
            _user = httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("HTTP context user is not available");
            _logger = logger;
        }

        public async Task<Tuple<List<ShoppingList>, int>> GetShoppingLists(DataTableJSONViewModel reqBody)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var query = _dbContext.ShoppingLists.AsQueryable();

                // Apply filters
                query = query.Where(l => l.UserId == userId);
                if (!string.IsNullOrEmpty(reqBody.search))
                {
                    query = query.Where(l => EF.Functions.Like(l.Name, "%" + reqBody.search + "%"));
                }

                // Apply ordering
                query = reqBody.order.ToLower() == "desc" 
                    ? query.OrderByDescending(l => EF.Property<object>(l, reqBody.orderBy))
                    : query.OrderBy(l => EF.Property<object>(l, reqBody.orderBy));

                var count = await query.CountAsync();
                var lists = await query
                    .Include(l => l.Items)
                    .Skip(reqBody.skip)
                    .Take(reqBody.pageSize)
                    .ToListAsync();

                return Tuple.Create(lists, count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shopping lists");
                throw;
            }
        }

        public async Task<ShoppingList?> GetShoppingList(int id)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                return await _dbContext.ShoppingLists
                    .Include(l => l.Items)
                    .FirstOrDefaultAsync(x => x.ShoppingListId == id && x.UserId == userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting shopping list {Id}", id);
                throw;
            }
        }

        public async Task<ShoppingList> AddShoppingList(ShoppingList model)
        {
            try
            {
                model.UserId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                SetCreatedOrModifiedBy(model, true);
                _dbContext.ShoppingLists.Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding shopping list");
                throw;
            }
        }

        public async Task<ShoppingList?> UpdateShoppingList(ShoppingList model)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var list = await _dbContext.ShoppingLists
                    .FirstOrDefaultAsync(l => l.ShoppingListId == model.ShoppingListId && l.UserId == userId);
                
                if (list == null)
                    return null;

                list.Name = model.Name;
                list.Description = model.Description;
                list.StatusId = model.StatusId;

                SetCreatedOrModifiedBy(list, false);
                _dbContext.ShoppingLists.Update(list);
                await _dbContext.SaveChangesAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shopping list {Id}", model.ShoppingListId);
                throw;
            }
        }

        public async Task<bool> DeleteShoppingList(int id)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var list = await _dbContext.ShoppingLists
                    .FirstOrDefaultAsync(l => l.ShoppingListId == id && l.UserId == userId);
                
                if (list == null)
                    return false;

                _dbContext.ShoppingLists.Remove(list);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting shopping list {Id}", id);
                throw;
            }
        }

        public async Task<ShoppingListItem?> AddShoppingListItem(ShoppingListItem model)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var list = await _dbContext.ShoppingLists
                    .FirstOrDefaultAsync(l => l.ShoppingListId == model.ShoppingListId && l.UserId == userId);
                
                if (list == null)
                    return null;

                SetCreatedOrModifiedBy(model, true);
                _dbContext.ShoppingListItems.Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding shopping list item");
                throw;
            }
        }

        public async Task<ShoppingListItem?> UpdateShoppingListItem(ShoppingListItem model)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var list = await _dbContext.ShoppingLists
                    .FirstOrDefaultAsync(l => l.ShoppingListId == model.ShoppingListId && l.UserId == userId);
                
                if (list == null)
                    return null;

                var item = await _dbContext.ShoppingListItems
                    .FirstOrDefaultAsync(i => i.ShoppingListItemId == model.ShoppingListItemId);
                
                if (item == null)
                    return null;

                item.ItemName = model.ItemName;
                item.Quantity = model.Quantity;
                item.Notes = model.Notes;
                item.IsCompleted = model.IsCompleted;

                SetCreatedOrModifiedBy(item, false);
                _dbContext.ShoppingListItems.Update(item);
                await _dbContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating shopping list item {Id}", model.ShoppingListItemId);
                throw;
            }
        }

        public async Task<bool> DeleteShoppingListItem(int id)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var item = await _dbContext.ShoppingListItems
                    .Include(i => i.ShoppingList)
                    .FirstOrDefaultAsync(i => i.ShoppingListItemId == id && i.ShoppingList.UserId == userId);
                
                if (item == null)
                    return false;

                _dbContext.ShoppingListItems.Remove(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting shopping list item {Id}", id);
                throw;
            }
        }

        public async Task<bool> ToggleItemCompletion(int id)
        {
            try
            {
                var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
                var item = await _dbContext.ShoppingListItems
                    .Include(i => i.ShoppingList)
                    .FirstOrDefaultAsync(i => i.ShoppingListItemId == id && i.ShoppingList.UserId == userId);
                
                if (item == null)
                    return false;

                item.IsCompleted = !item.IsCompleted;
                SetCreatedOrModifiedBy(item, false);
                _dbContext.ShoppingListItems.Update(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error toggling item completion {Id}", id);
                throw;
            }
        }

        private void SetCreatedOrModifiedBy(CommonProperties model, bool isNew)
        {
            var userId = Convert.ToInt32(_user.FindFirstValue("UserId"));
            if (isNew)
            {
                model.CreatedBy = userId;
                model.CreatedOn = DateTime.Now;
            }
            else
            {
                model.ModifiedBy = userId;
                model.ModifiedOn = DateTime.Now;
            }
        }
    }
} 