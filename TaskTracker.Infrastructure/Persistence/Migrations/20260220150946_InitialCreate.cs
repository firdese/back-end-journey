using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTracker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "taskgroups",
                schema: "public",
                columns: table => new
                {
                    taskgroupid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    taskgroupdescription = table.Column<string>(type: "text", nullable: true),
                    taskgroupcreatedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    taskgroupupdatedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    taskgrouparchivedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taskgroupcolor = table.Column<string>(type: "text", nullable: true),
                    taskgroupsortorder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskgroups", x => x.taskgroupid);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                schema: "public",
                columns: table => new
                {
                    taskid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    taskdescription = table.Column<string>(type: "text", nullable: true),
                    taskcompletedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taskstartatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taskendatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taskprogress = table.Column<int>(type: "integer", nullable: true),
                    taskdueatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    taskdeletedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tasksortorder = table.Column<int>(type: "integer", nullable: false),
                    taskpriority = table.Column<short>(type: "smallint", nullable: false),
                    taskgroupid = table.Column<int>(type: "integer", nullable: false),
                    taskcreatedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    taskupdatedatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.taskid);
                    table.ForeignKey(
                        name: "FK_tasks_taskgroups_taskgroupid",
                        column: x => x.taskgroupid,
                        principalSchema: "public",
                        principalTable: "taskgroups",
                        principalColumn: "taskgroupid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "taskdependencies",
                schema: "public",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    DependsOnTaskId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskdependencies", x => new { x.TaskId, x.DependsOnTaskId });
                    table.CheckConstraint("chk_no_self_dependency", "\"TaskId\" <> \"DependsOnTaskId\"");
                    table.ForeignKey(
                        name: "FK_taskdependencies_tasks_DependsOnTaskId",
                        column: x => x.DependsOnTaskId,
                        principalSchema: "public",
                        principalTable: "tasks",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_taskdependencies_tasks_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "public",
                        principalTable: "tasks",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskdependencies_DependsOnTaskId",
                schema: "public",
                table: "taskdependencies",
                column: "DependsOnTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_taskgroupid",
                schema: "public",
                table: "tasks",
                column: "taskgroupid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskdependencies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tasks",
                schema: "public");

            migrationBuilder.DropTable(
                name: "taskgroups",
                schema: "public");
        }
    }
}
