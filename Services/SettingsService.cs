using Microsoft.EntityFrameworkCore;
using shopping_list_api.Models;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace shopping_list_api.Services
{
    public class SettingsService : ISettings
    {
        private readonly ShoppingListDbContext _dbContext;
        private readonly ClaimsPrincipal _user;
        private readonly ILogger<SettingsService> _logger;

        public SettingsService(IHttpContextAccessor httpContextAccessor, ShoppingListDbContext dbContext, ILogger<SettingsService> logger)
        {
            _dbContext = dbContext;
            _user = httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("HTTP context user is not available");
            _logger = logger;
        }

        public async Task<dynamic> GetSettings()
        {
            try
            {
                var records = await _dbContext.Settings
                    .Select(s => new
                    {
                        s.SettingId,
                        s.SettingName,
                        s.SettingValue,
                        s.SettingType,
                    })
                    .ToListAsync();

                return records;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting settings");
                throw;
            }
        }

        public async Task<bool> SaveSetting(int settingId, string settingValue)
        {
            try
            {
                var model = await _dbContext.Settings.FirstOrDefaultAsync(s => s.SettingId == settingId);
                if (model == null)
                {
                    return false;
                }

                model.SettingValue = settingValue;
                SetCreatedOrModifiedBy(model, false);
                _dbContext.Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving setting {SettingId}", settingId);
                throw;
            }
        }

        private void SetCreatedOrModifiedBy(Settings model, bool isNew)
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