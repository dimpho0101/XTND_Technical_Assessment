using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XTND_Technical_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class CreateTasksTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "task_users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TaskUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskCreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskUpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tasks_task_users_TaskUserId",
                        column: x => x.TaskUserId,
                        principalTable: "task_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tasks_TaskUserId",
                table: "tasks",
                column: "TaskUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "task_users");
        }
    }
}
