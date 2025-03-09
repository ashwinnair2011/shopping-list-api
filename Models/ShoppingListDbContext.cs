using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace shopping_list_api.Models
{
    public partial class ShoppingListDbContext : DbContext
    {
        public ShoppingListDbContext()
        {
        }

        public ShoppingListDbContext(DbContextOptions<ShoppingListDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).HasMaxLength(250);
                entity.Property(e => e.UserPassword).HasMaxLength(250);
                entity.Property(e => e.UserToken).HasMaxLength(1000);
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(u => u.Permissions)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
                    );
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.PermissionId);
                entity.Property(e => e.PermissionName).HasMaxLength(250);
                entity.Property(e => e.PermissionDesc).HasMaxLength(250);
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => e.UserPermissionId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.PermissionId);
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.HasKey(e => e.SettingId);
                entity.Property(e => e.SettingName).HasMaxLength(255);
                entity.Property(e => e.SettingValue).HasMaxLength(255);
                entity.Property(e => e.SettingType).HasMaxLength(25);
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasKey(e => e.ShoppingListId);
                entity.Property(e => e.Name).HasMaxLength(250);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.StatusId);
                entity.HasMany(sl => sl.Items)
                    .WithOne()
                    .HasForeignKey(sli => sli.ShoppingListId);
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(e => e.ShoppingListItemId);
                entity.Property(e => e.ItemName).HasMaxLength(250);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.HasIndex(e => e.ShoppingListId);
                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.ShoppingListId);
            });

            // Seed default system user
            var defaultUser = new User
            {
                UserId = 1,
                UserName = "system.user",
                UserPassword = BCrypt.Net.BCrypt.HashPassword("system.user"),
                Permissions = new List<string> { 
                    "ADMIN", 
                    "canLogInToApi",
                    "canViewUsers",
                    "canModifyUsers",
                    "canViewShoppingLists",
                    "canModifyShoppingLists",
                    "canViewSettings",
                    "canModifySettings"
                },
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            // Seed permissions
            var adminPermission = new Permission
            {
                PermissionId = 1,
                PermissionName = "ADMIN",
                PermissionDesc = "Administrator permission",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var loginPermission = new Permission
            {
                PermissionId = 2,
                PermissionName = "canLogInToApi",
                PermissionDesc = "Can log in to API",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var viewUsersPermission = new Permission
            {
                PermissionId = 3,
                PermissionName = "canViewUsers",
                PermissionDesc = "Can view users",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var modifyUsersPermission = new Permission
            {
                PermissionId = 4,
                PermissionName = "canModifyUsers",
                PermissionDesc = "Can modify users",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var viewShoppingListsPermission = new Permission
            {
                PermissionId = 5,
                PermissionName = "canViewShoppingLists",
                PermissionDesc = "Can view shopping lists",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var modifyShoppingListsPermission = new Permission
            {
                PermissionId = 6,
                PermissionName = "canModifyShoppingLists",
                PermissionDesc = "Can modify shopping lists",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var viewSettingsPermission = new Permission
            {
                PermissionId = 7,
                PermissionName = "canViewSettings",
                PermissionDesc = "Can view settings",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            var modifySettingsPermission = new Permission
            {
                PermissionId = 8,
                PermissionName = "canModifySettings",
                PermissionDesc = "Can modify settings",
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow
            };

            // Seed user permission mappings
            var userAdminPermission = new UserPermission
            {
                UserPermissionId = 1,
                UserId = 1,
                PermissionId = 1
            };

            var userLoginPermission = new UserPermission
            {
                UserPermissionId = 2,
                UserId = 1,
                PermissionId = 2
            };

            var userViewUsersPermission = new UserPermission
            {
                UserPermissionId = 3,
                UserId = 1,
                PermissionId = 3
            };

            var userModifyUsersPermission = new UserPermission
            {
                UserPermissionId = 4,
                UserId = 1,
                PermissionId = 4
            };

            var userViewShoppingListsPermission = new UserPermission
            {
                UserPermissionId = 5,
                UserId = 1,
                PermissionId = 5
            };

            var userModifyShoppingListsPermission = new UserPermission
            {
                UserPermissionId = 6,
                UserId = 1,
                PermissionId = 6
            };

            var userViewSettingsPermission = new UserPermission
            {
                UserPermissionId = 7,
                UserId = 1,
                PermissionId = 7
            };

            var userModifySettingsPermission = new UserPermission
            {
                UserPermissionId = 8,
                UserId = 1,
                PermissionId = 8
            };

            modelBuilder.Entity<User>().HasData(defaultUser);
            modelBuilder.Entity<Permission>().HasData(
                adminPermission, 
                loginPermission,
                viewUsersPermission,
                modifyUsersPermission,
                viewShoppingListsPermission,
                modifyShoppingListsPermission,
                viewSettingsPermission,
                modifySettingsPermission
            );
            modelBuilder.Entity<UserPermission>().HasData(
                userAdminPermission,
                userLoginPermission,
                userViewUsersPermission,
                userModifyUsersPermission,
                userViewShoppingListsPermission,
                userModifyShoppingListsPermission,
                userViewSettingsPermission,
                userModifySettingsPermission
            );

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
} 