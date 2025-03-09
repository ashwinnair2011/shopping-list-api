using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shopping_list_api.Helper;
using shopping_list_api.Services;

namespace shopping_list_api.Controllers
{
    [Route("api/settings")]
    [ApiController]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettings _settings;
        private readonly ILogger<SettingsController> _logger;

        public SettingsController(ISettings settings, ILogger<SettingsController> logger)
        {
            _settings = settings;
            _logger = logger;
        }

        [HttpGet()]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canViewSettings)]
        public async Task<IActionResult> getSettings()
        {
            try
            {
                var result = await _settings.GetSettings();
                return Ok(new { success = true, result = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }

        [HttpPut("")]
        [ShoppingListAuthorizeAttribute(Enumerations.Permissions.canModifySettings)]
        public async Task<IActionResult> saveSettings(int settingId, string settingValue)
        {
            try
            {
                var result = await _settings.SaveSetting(settingId, settingValue);
                if (result)
                    return Ok(new { success = true, result = "Setting updated" });
                else
                    return NotFound(new { success = false, result = "Invalid setting information" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new { success = false, result = ex.Message });
            }
        }
    }
} 