using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookmarks.Data.Migrations
{
    public partial class BMIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_Url",
                schema: "Info",
                table: "Bookmarks");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_Url_UserId",
                schema: "Info",
                table: "Bookmarks",
                columns: new[] { "Url", "UserId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_Url_UserId",
                schema: "Info",
                table: "Bookmarks");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_Url",
                schema: "Info",
                table: "Bookmarks",
                column: "Url",
                unique: true);
        }
    }
}
