﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shopping_list_api.Models;

#nullable disable

namespace shopping_list_api.Migrations
{
    [DbContext(typeof(ShoppingListDbContext))]
    [Migration("20250309125457_AddAllPermissions")]
    partial class AddAllPermissions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("shopping_list_api.Models.Permission", b =>
                {
                    b.Property<int>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PermissionDesc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("PermissionName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9818),
                            PermissionDesc = "Administrator permission",
                            PermissionName = "ADMIN"
                        },
                        new
                        {
                            PermissionId = 2,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9821),
                            PermissionDesc = "Can log in to API",
                            PermissionName = "canLogInToApi"
                        },
                        new
                        {
                            PermissionId = 3,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9823),
                            PermissionDesc = "Can view users",
                            PermissionName = "canViewUsers"
                        },
                        new
                        {
                            PermissionId = 4,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9826),
                            PermissionDesc = "Can modify users",
                            PermissionName = "canModifyUsers"
                        },
                        new
                        {
                            PermissionId = 5,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9828),
                            PermissionDesc = "Can view shopping lists",
                            PermissionName = "canViewShoppingLists"
                        },
                        new
                        {
                            PermissionId = 6,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9830),
                            PermissionDesc = "Can modify shopping lists",
                            PermissionName = "canModifyShoppingLists"
                        },
                        new
                        {
                            PermissionId = 7,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9832),
                            PermissionDesc = "Can view settings",
                            PermissionName = "canViewSettings"
                        },
                        new
                        {
                            PermissionId = 8,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9834),
                            PermissionDesc = "Can modify settings",
                            PermissionName = "canModifySettings"
                        });
                });

            modelBuilder.Entity("shopping_list_api.Models.Settings", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SettingName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SettingType")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("SettingValue")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("shopping_list_api.Models.ShoppingList", b =>
                {
                    b.Property<int>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingListId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("shopping_list_api.Models.ShoppingListItem", b =>
                {
                    b.Property<int>("ShoppingListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingListId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingListItemId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ShoppingListItems");
                });

            modelBuilder.Entity("shopping_list_api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("UserToken")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.HasKey("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            CreatedBy = 1,
                            CreatedOn = new DateTime(2025, 3, 9, 12, 54, 56, 452, DateTimeKind.Utc).AddTicks(9807),
                            Permissions = "[\"ADMIN\",\"canLogInToApi\",\"canViewUsers\",\"canModifyUsers\",\"canViewShoppingLists\",\"canModifyShoppingLists\",\"canViewSettings\",\"canModifySettings\"]",
                            UserName = "system.user",
                            UserPassword = "$2a$11$.yKLsBulzZew0UoQ1tZrQuy/B9jf5AgTy/Em.TZYlzH4ovXdGgVNG"
                        });
                });

            modelBuilder.Entity("shopping_list_api.Models.UserPermission", b =>
                {
                    b.Property<int>("UserPermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserPermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");

                    b.HasData(
                        new
                        {
                            UserPermissionId = 1,
                            PermissionId = 1,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 2,
                            PermissionId = 2,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 3,
                            PermissionId = 3,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 4,
                            PermissionId = 4,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 5,
                            PermissionId = 5,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 6,
                            PermissionId = 6,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 7,
                            PermissionId = 7,
                            UserId = 1
                        },
                        new
                        {
                            UserPermissionId = 8,
                            PermissionId = 8,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("shopping_list_api.Models.ShoppingListItem", b =>
                {
                    b.HasOne("shopping_list_api.Models.ShoppingList", "ShoppingList")
                        .WithMany("Items")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("shopping_list_api.Models.ShoppingList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
