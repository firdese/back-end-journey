using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskGroupEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskGroupId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TaskGroups",
                columns: table => new
                {
                    TaskGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskGroups", x => x.TaskGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskGroupId",
                table: "Tasks",
                column: "TaskGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskGroups_TaskGroupId",
                table: "Tasks",
                column: "TaskGroupId",
                principalTable: "TaskGroups",
                principalColumn: "TaskGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskGroups_TaskGroupId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskGroups");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskGroupId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskGroupId",
                table: "Tasks");
        }
    }
}
