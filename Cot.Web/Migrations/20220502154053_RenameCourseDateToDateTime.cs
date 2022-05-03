using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cot.Web.Migrations
{
    public partial class RenameCourseDateToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDateTime",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Courses",
                nullable: true);

            migrationBuilder.Sql("UPDATE Courses SET AddedDateTime = AddedDate, ModifiedDateTime = ModifiedDate;");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Courses");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDateTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Courses");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);
        }
    }
}
