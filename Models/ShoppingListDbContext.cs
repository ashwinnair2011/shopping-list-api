using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).HasMaxLength(250);
                entity.Property(e => e.UserPassword).HasMaxLength(250);
                entity.Property(e => e.UserToken).HasMaxLength(1000);
                entity.HasIndex(e => e.UserName).IsUnique();
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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
} 