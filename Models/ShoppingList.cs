namespace shopping_list_api.Models
{
    public partial class ShoppingList : CommonProperties
    {
        public int ShoppingListId { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int StatusId { get; set; }

        public int UserId { get; set; }

        public required List<ShoppingListItem> Items { get; set; } = new();
    }

    public partial class ShoppingListItem : CommonProperties
    {
        public int ShoppingListItemId { get; set; }

        public int ShoppingListId { get; set; }

        public required string ItemName { get; set; }

        public int Quantity { get; set; }

        public string? Notes { get; set; }

        public bool IsCompleted { get; set; }

        public required ShoppingList ShoppingList { get; set; }
    }
} 