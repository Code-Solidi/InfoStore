using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookmarks.Data.Migrations
{
    public partial class BookmarksInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Info");

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

            migrationBuilder.CreateTable(
                name: "Tags",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(2083)", maxLength: 2083, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Groups_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Info",
                        principalTable: "Groups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookmarkTag",
                schema: "Info",
                columns: table => new
                {
                    BookmarksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookmarkTag", x => new { x.BookmarksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BookmarkTag_Bookmarks_BookmarksId",
                        column: x => x.BookmarksId,
                        principalSchema: "Info",
                        principalTable: "Bookmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookmarkTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalSchema: "Info",
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_BookmarkTag_TagsId",
                schema: "Info",
                table: "BookmarkTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                schema: "Info",
                table: "Groups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Text",
                schema: "Info",
                table: "Tags",
                column: "Text",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookmarkTag",
                schema: "Info");

            migrationBuilder.DropTable(
                name: "Bookmarks",
                schema: "Info");

            migrationBuilder.DropTable(
                name: "Tags",
                schema: "Info");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Info");
        }
    }
}
