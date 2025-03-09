namespace shopping_list_api.Models
{
    public partial class Permission : CommonProperties
    {
        public int PermissionId { get; set; }

        public string PermissionName { get; set; } = null!;

        public string PermissionDesc { get; set; } = null!;
    }
} 