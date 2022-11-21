using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class ToDoNotified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Notified",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_Text",
                schema: "Info",
                table: "ToDos",
                column: "Text",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ToDos_Text",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Notified",
                schema: "Info",
                table: "ToDos");
        }
    }
}
