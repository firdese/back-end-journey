using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTracker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "taskattachments",
                schema: "public",
                columns: table => new
                {
                    taskattachmentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    taskid = table.Column<int>(type: "integer", nullable: false),
                    objectkey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    filename = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contenttype = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    sizebytes = table.Column<long>(type: "bigint", nullable: false),
                    createdatutc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taskattachments", x => x.taskattachmentid);
                    table.ForeignKey(
                        name: "FK_taskattachments_tasks_taskid",
                        column: x => x.taskid,
                        principalSchema: "public",
                        principalTable: "tasks",
                        principalColumn: "taskid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_taskattachments_taskid",
                schema: "public",
                table: "taskattachments",
                column: "taskid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "taskattachments",
                schema: "public");
        }
    }
}
