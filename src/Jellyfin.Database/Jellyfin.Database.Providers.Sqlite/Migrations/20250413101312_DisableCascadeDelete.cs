using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jellyfin.Server.Implementations.Migrations
{
    /// <inheritdoc />
    public partial class DisableCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AncestorIds_BaseItems_ItemId",
                table: "AncestorIds");

            migrationBuilder.DropForeignKey(
                name: "FK_AncestorIds_BaseItems_ParentItemId",
                table: "AncestorIds");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentStreamInfos_BaseItems_ItemId",
                table: "AttachmentStreamInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemImageInfos_BaseItems_ItemId",
                table: "BaseItemImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemMetadataFields_BaseItems_ItemId",
                table: "BaseItemMetadataFields");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemProviders_BaseItems_ItemId",
                table: "BaseItemProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemTrailerTypes_BaseItems_ItemId",
                table: "BaseItemTrailerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_BaseItems_ItemId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemValuesMap_BaseItems_ItemId",
                table: "ItemValuesMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemValuesMap_ItemValues_ItemValueId",
                table: "ItemValuesMap");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyframeData_BaseItems_ItemId",
                table: "KeyframeData");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaStreamInfos_BaseItems_ItemId",
                table: "MediaStreamInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleBaseItemMap_BaseItems_ItemId",
                table: "PeopleBaseItemMap");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleBaseItemMap_Peoples_PeopleId",
                table: "PeopleBaseItemMap");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_BaseItems_ItemId",
                table: "UserData");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Users_UserId",
                table: "UserData");

            migrationBuilder.AddForeignKey(
                name: "FK_AncestorIds_BaseItems_ItemId",
                table: "AncestorIds",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AncestorIds_BaseItems_ParentItemId",
                table: "AncestorIds",
                column: "ParentItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentStreamInfos_BaseItems_ItemId",
                table: "AttachmentStreamInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemImageInfos_BaseItems_ItemId",
                table: "BaseItemImageInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemMetadataFields_BaseItems_ItemId",
                table: "BaseItemMetadataFields",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemProviders_BaseItems_ItemId",
                table: "BaseItemProviders",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemTrailerTypes_BaseItems_ItemId",
                table: "BaseItemTrailerTypes",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_BaseItems_ItemId",
                table: "Chapters",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemValuesMap_BaseItems_ItemId",
                table: "ItemValuesMap",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemValuesMap_ItemValues_ItemValueId",
                table: "ItemValuesMap",
                column: "ItemValueId",
                principalTable: "ItemValues",
                principalColumn: "ItemValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyframeData_BaseItems_ItemId",
                table: "KeyframeData",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaStreamInfos_BaseItems_ItemId",
                table: "MediaStreamInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleBaseItemMap_BaseItems_ItemId",
                table: "PeopleBaseItemMap",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleBaseItemMap_Peoples_PeopleId",
                table: "PeopleBaseItemMap",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_BaseItems_ItemId",
                table: "UserData",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Users_UserId",
                table: "UserData",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AncestorIds_BaseItems_ItemId",
                table: "AncestorIds");

            migrationBuilder.DropForeignKey(
                name: "FK_AncestorIds_BaseItems_ParentItemId",
                table: "AncestorIds");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentStreamInfos_BaseItems_ItemId",
                table: "AttachmentStreamInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemImageInfos_BaseItems_ItemId",
                table: "BaseItemImageInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemMetadataFields_BaseItems_ItemId",
                table: "BaseItemMetadataFields");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemProviders_BaseItems_ItemId",
                table: "BaseItemProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseItemTrailerTypes_BaseItems_ItemId",
                table: "BaseItemTrailerTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Chapters_BaseItems_ItemId",
                table: "Chapters");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemValuesMap_BaseItems_ItemId",
                table: "ItemValuesMap");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemValuesMap_ItemValues_ItemValueId",
                table: "ItemValuesMap");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyframeData_BaseItems_ItemId",
                table: "KeyframeData");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaStreamInfos_BaseItems_ItemId",
                table: "MediaStreamInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleBaseItemMap_BaseItems_ItemId",
                table: "PeopleBaseItemMap");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleBaseItemMap_Peoples_PeopleId",
                table: "PeopleBaseItemMap");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_BaseItems_ItemId",
                table: "UserData");

            migrationBuilder.DropForeignKey(
                name: "FK_UserData_Users_UserId",
                table: "UserData");

            migrationBuilder.AddForeignKey(
                name: "FK_AncestorIds_BaseItems_ItemId",
                table: "AncestorIds",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AncestorIds_BaseItems_ParentItemId",
                table: "AncestorIds",
                column: "ParentItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentStreamInfos_BaseItems_ItemId",
                table: "AttachmentStreamInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemImageInfos_BaseItems_ItemId",
                table: "BaseItemImageInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemMetadataFields_BaseItems_ItemId",
                table: "BaseItemMetadataFields",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemProviders_BaseItems_ItemId",
                table: "BaseItemProviders",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseItemTrailerTypes_BaseItems_ItemId",
                table: "BaseItemTrailerTypes",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_BaseItems_ItemId",
                table: "Chapters",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemValuesMap_BaseItems_ItemId",
                table: "ItemValuesMap",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemValuesMap_ItemValues_ItemValueId",
                table: "ItemValuesMap",
                column: "ItemValueId",
                principalTable: "ItemValues",
                principalColumn: "ItemValueId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyframeData_BaseItems_ItemId",
                table: "KeyframeData",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaStreamInfos_BaseItems_ItemId",
                table: "MediaStreamInfos",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleBaseItemMap_BaseItems_ItemId",
                table: "PeopleBaseItemMap",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleBaseItemMap_Peoples_PeopleId",
                table: "PeopleBaseItemMap",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_BaseItems_ItemId",
                table: "UserData",
                column: "ItemId",
                principalTable: "BaseItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserData_Users_UserId",
                table: "UserData",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
