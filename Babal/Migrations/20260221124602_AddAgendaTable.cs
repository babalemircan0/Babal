using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Babal.Migrations
{
    /// <inheritdoc />
    public partial class AddAgendaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendas_Users_UserId",
                table: "Agendas");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropIndex(
                name: "IX_Agendas_UserId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "DayName",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "TimeSlot",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Agendas");

            migrationBuilder.AddColumn<string>(
                name: "CellId",
                table: "Agendas",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "Agendas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellId",
                table: "Agendas");

            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "Agendas");

            migrationBuilder.AddColumn<string>(
                name: "DayName",
                table: "Agendas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Agendas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TimeSlot",
                table: "Agendas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Agendas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<int>(type: "INTEGER", nullable: true),
                    MessageText = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverId = table.Column<int>(type: "INTEGER", nullable: true),
                    SentDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendas_UserId",
                table: "Agendas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendas_Users_UserId",
                table: "Agendas",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
