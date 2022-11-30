using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class Groups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "Info",
                table: "Bookmarks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Text",
                schema: "Info",
                table: "Tags",
                column: "Text",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_GroupId",
                schema: "Info",
                table: "Bookmarks",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_Url",
                schema: "Info",
                table: "Bookmarks",
                column: "Url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                schema: "Info",
                table: "Groups",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Groups_GroupId",
                schema: "Info",
                table: "Bookmarks",
                column: "GroupId",
                principalSchema: "Info",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Groups_GroupId",
                schema: "Info",
                table: "Bookmarks");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Info");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Text",
                schema: "Info",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_GroupId",
                schema: "Info",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_Url",
                schema: "Info",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "Info",
                table: "Bookmarks");
        }
    }
}
