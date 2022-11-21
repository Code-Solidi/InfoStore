using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class StatusToDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                schema: "Info",
                table: "ToDos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
