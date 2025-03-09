namespace shopping_list_api.Enumerations
{
    [Flags]
    public enum Permissions : int
    {
        None = 0,
        canLogInToApi = 1,
        canViewUsers = 2,
        canModifyUsers = 3,
        canViewShoppingLists = 4,
        canModifyShoppingLists = 5,
        canViewSettings = 6,
        canModifySettings = 7
    }
} 