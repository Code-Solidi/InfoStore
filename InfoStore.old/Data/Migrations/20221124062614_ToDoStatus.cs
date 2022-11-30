using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class ToDoStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Info",
                table: "ToDos");
        }
    }
}
