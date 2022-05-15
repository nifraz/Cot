using Microsoft.EntityFrameworkCore.Migrations;

namespace Cot.Data.Migrations
{
    public partial class AddedCourseNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Courses");
        }
    }
}
