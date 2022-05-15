using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cot.Data.Migrations
{
    public partial class RenameDateTimeToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Courses",
                nullable: true);

            migrationBuilder.Sql("UPDATE Courses SET AddedDate = AddedDateTime, ModifiedDate = ModifiedDateTime;");

            migrationBuilder.DropColumn(
                name: "AddedDateTime",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Courses");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDateTime",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Courses",
                type: "datetime2",
                nullable: true);
        }
    }
}
