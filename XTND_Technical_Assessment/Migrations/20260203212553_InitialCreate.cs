using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 

namespace XTND_Technical_Assessment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "task_item_statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_item_statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "task_users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TaskUserId = table.Column<int>(type: "integer", nullable: false),
                    TaskStatusId = table.Column<int>(type: "integer", nullable: false),
                    TaskCreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskUpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskItemStatusId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tasks_task_item_statuses_TaskItemStatusId",
                        column: x => x.TaskItemStatusId,
                        principalTable: "task_item_statuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tasks_task_item_statuses_TaskStatusId",
                        column: x => x.TaskStatusId,
                        principalTable: "task_item_statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tasks_task_users_TaskUserId",
                        column: x => x.TaskUserId,
                        principalTable: "task_users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "task_item_statuses",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Backlog" },
                    { 2, true, "In Progress" },
                    { 3, true, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_item_statuses_Name",
                table: "task_item_statuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_TaskItemStatusId",
                table: "tasks",
                column: "TaskItemStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_TaskStatusId",
                table: "tasks",
                column: "TaskStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_TaskUserId",
                table: "tasks",
                column: "TaskUserId");
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "task_item_statuses");

            migrationBuilder.DropTable(
                name: "task_users");
        }
    }
}
