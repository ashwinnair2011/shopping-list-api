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
                Permissions = new List<string> { "ADMIN", "canLogInToApi" },
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

            modelBuilder.Entity<User>().HasData(defaultUser);
            modelBuilder.Entity<Permission>().HasData(adminPermission, loginPermission);
            modelBuilder.Entity<UserPermission>().HasData(userAdminPermission, userLoginPermission);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
} 