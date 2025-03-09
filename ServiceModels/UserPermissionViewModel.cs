namespace shopping_list_api.ServiceModels
{
    public class UserPermissionViewModel
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required List<string> Permissions { get; set; }
    }

    public class UserPermissionIntViewModel
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required List<int> Permissions { get; set; }
    }

    public class DataTableJSONViewModel
    {
        public required string orderBy { get; set; }
        public required string order { get; set; }
        public string? search { get; set; }
        public int skip { get; set; }
        public int pageSize { get; set; }
    }

    public class DropdownViewModel
    {
        public int Value { get; set; }
        public required string Label { get; set; }
    }
} 