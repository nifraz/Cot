using Microsoft.EntityFrameworkCore.Migrations;

namespace Cot.Data.Migrations
{
    public partial class RenameCourseTypeToCourseLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Courses");

            migrationBuilder.AddColumn<byte>(
                name: "Level",
                table: "Courses",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Courses");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                table: "Courses",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
