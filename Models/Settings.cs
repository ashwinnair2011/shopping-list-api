namespace shopping_list_api.Models
{
    public class Settings : CommonProperties
    {
        public int SettingId { get; set; }
        public required string SettingName { get; set; }
        public required string SettingValue { get; set; }
        public required string SettingType { get; set; }
    }
} 