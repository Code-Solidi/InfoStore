using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class Reminders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTime",
                schema: "Info",
                table: "ToDos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ReminderId",
                schema: "Info",
                table: "ToDos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Reminders",
                schema: "Info",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MinutesHoursDays = table.Column<int>(type: "int", nullable: false),
                    Before = table.Column<long>(type: "bigint", nullable: false),
                    Repeat = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_ReminderId",
                schema: "Info",
                table: "ToDos",
                column: "ReminderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Reminders_ReminderId",
                schema: "Info",
                table: "ToDos",
                column: "ReminderId",
                principalSchema: "Info",
                principalTable: "Reminders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Reminders_ReminderId",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropTable(
                name: "Reminders",
                schema: "Info");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_ReminderId",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "DueDateTime",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "ReminderId",
                schema: "Info",
                table: "ToDos");
        }
    }
}
