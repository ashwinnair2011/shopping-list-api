namespace shopping_list_api.Models
{
    public partial class User : CommonProperties
    {
        public int UserId { get; set; }

        public required string UserName { get; set; }

        public required string UserPassword { get; set; }

        public string? UserToken { get; set; }

        public required List<string> Permissions { get; set; } = new();
    }
} 