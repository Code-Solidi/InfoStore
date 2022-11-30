using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfoStore.Data.Migrations
{
    public partial class NoReminder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ReminderId",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                schema: "Info",
                table: "ToDos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Remind",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Repeat",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeUnit",
                schema: "Info",
                table: "ToDos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EMail",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Remind",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "Repeat",
                schema: "Info",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "TimeUnit",
                schema: "Info",
                table: "ToDos");

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
                    Before = table.Column<long>(type: "bigint", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MinutesHoursDays = table.Column<int>(type: "int", nullable: false),
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
    }
}
