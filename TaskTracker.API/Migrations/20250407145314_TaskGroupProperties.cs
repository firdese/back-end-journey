using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class TaskGroupProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskGroupDescription",
                table: "TaskGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskGroupDescription",
                table: "TaskGroups");
        }
    }
}
