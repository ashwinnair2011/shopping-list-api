namespace shopping_list_api.Models
{
    public partial class UserPermission
    {
        public int UserPermissionId { get; set; }

        public int UserId { get; set; }

        public int PermissionId { get; set; }
    }
} 