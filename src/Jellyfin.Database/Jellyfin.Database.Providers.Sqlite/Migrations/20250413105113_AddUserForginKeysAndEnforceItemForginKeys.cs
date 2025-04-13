using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jellyfin.Server.Implementations.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForginKeysAndEnforceItemForginKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            """
            DELETE FROM "DisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "Users" WHERE "DisplayPreferences"."UserId" = "Users"."Id");
            DELETE FROM "DisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "BaseItems" WHERE "DisplayPreferences"."ItemId" = "BaseItems"."Id");
            DELETE FROM "HomeSection" WHERE NOT EXISTS(SELECT 1 FROM "DisplayPreferences" WHERE "HomeSection"."DisplayPreferencesId" = "DisplayPreferences"."Id");

            DELETE FROM "ItemDisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "Users" WHERE "ItemDisplayPreferences"."UserId" = "Users"."Id");
            DELETE FROM "ItemDisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "BaseItems" WHERE "ItemDisplayPreferences"."ItemId" = "BaseItems"."Id");

            DELETE FROM "CustomItemDisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "Users" WHERE "CustomItemDisplayPreferences"."UserId" = "Users"."Id");
            DELETE FROM "CustomItemDisplayPreferences" WHERE NOT EXISTS(SELECT 1 FROM "BaseItems" WHERE "CustomItemDisplayPreferences"."ItemId" = "BaseItems"."Id");
            """);

            migrationBuilder.DropForeignKey(
                name: "FK_AccessSchedules_Users_UserId",
                table: "AccessSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_DisplayPreferences_Users_UserId",
                table: "DisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeSection_DisplayPreferences_DisplayPreferencesId",
                table: "HomeSection");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_Users_UserId",
                table: "ImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisplayPreferences_Users_UserId",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemDisplayPreferences",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseItemImageInfos",
                table: "BaseItemImageInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Preferences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Permissions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemDisplayPreferences",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemDisplayPreferences",
                table: "ItemDisplayPreferences",
                columns: new[] { "Id", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseItemImageInfos",
                table: "BaseItemImageInfos",
                columns: new[] { "Id", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_UserId1",
                table: "Preferences",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId1",
                table: "Permissions",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDisplayPreferences_ItemId",
                table: "ItemDisplayPreferences",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DisplayPreferences_ItemId",
                table: "DisplayPreferences",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomItemDisplayPreferences_ItemId",
                table: "CustomItemDisplayPreferences",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessSchedules_Users_UserId",
                table: "AccessSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomItemDisplayPreferences_BaseItems_ItemId",
                table: "CustomItemDisplayPreferences",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomItemDisplayPreferences_Users_UserId",
                table: "CustomItemDisplayPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisplayPreferences_BaseItems_ItemId",
                table: "DisplayPreferences",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisplayPreferences_Users_UserId",
                table: "DisplayPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSection_DisplayPreferences_DisplayPreferencesId",
                table: "HomeSection",
                column: "DisplayPreferencesId",
                principalTable: "DisplayPreferences",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_Users_UserId",
                table: "ImageInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisplayPreferences_BaseItems_ItemId",
                table: "ItemDisplayPreferences",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisplayPreferences_Users_UserId",
                table: "ItemDisplayPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId1",
                table: "Permissions",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_UserId1",
                table: "Preferences",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessSchedules_Users_UserId",
                table: "AccessSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomItemDisplayPreferences_BaseItems_ItemId",
                table: "CustomItemDisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomItemDisplayPreferences_Users_UserId",
                table: "CustomItemDisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_DisplayPreferences_BaseItems_ItemId",
                table: "DisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_DisplayPreferences_Users_UserId",
                table: "DisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeSection_DisplayPreferences_DisplayPreferencesId",
                table: "HomeSection");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageInfos_Users_UserId",
                table: "ImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisplayPreferences_BaseItems_ItemId",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemDisplayPreferences_Users_UserId",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Users_UserId1",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Preferences_Users_UserId1",
                table: "Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Preferences_UserId1",
                table: "Preferences");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_UserId1",
                table: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemDisplayPreferences",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropIndex(
                name: "IX_ItemDisplayPreferences_ItemId",
                table: "ItemDisplayPreferences");

            migrationBuilder.DropIndex(
                name: "IX_DisplayPreferences_ItemId",
                table: "DisplayPreferences");

            migrationBuilder.DropIndex(
                name: "IX_CustomItemDisplayPreferences_ItemId",
                table: "CustomItemDisplayPreferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseItemImageInfos",
                table: "BaseItemImageInfos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Preferences");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Permissions");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ItemDisplayPreferences",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemDisplayPreferences",
                table: "ItemDisplayPreferences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseItemImageInfos",
                table: "BaseItemImageInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessSchedules_Users_UserId",
                table: "AccessSchedules",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisplayPreferences_Users_UserId",
                table: "DisplayPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeSection_DisplayPreferences_DisplayPreferencesId",
                table: "HomeSection",
                column: "DisplayPreferencesId",
                principalTable: "DisplayPreferences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageInfos_Users_UserId",
                table: "ImageInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemDisplayPreferences_Users_UserId",
                table: "ItemDisplayPreferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Users_UserId",
                table: "Permissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Preferences_Users_UserId",
                table: "Preferences",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
