namespace shopping_list_api.Services
{
    public interface ISettings
    {
        Task<dynamic> GetSettings();
        Task<bool> SaveSetting(int settingId, string settingValue);
    }
} 