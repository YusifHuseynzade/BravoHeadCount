using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateProjectColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaManagerEmail",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "StoreManagerEmail",
                table: "Projects",
                newName: "StoreManagerMail");

            migrationBuilder.RenameColumn(
                name: "RecruiterEmail",
                table: "Projects",
                newName: "RecruiterMail");

            migrationBuilder.RenameColumn(
                name: "DirectorEmail",
                table: "Projects",
                newName: "AreaManagerMail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 19, 11, 58, 27, 214, DateTimeKind.Utc).AddTicks(277),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 19, 11, 47, 21, 864, DateTimeKind.Utc).AddTicks(2558));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreManagerMail",
                table: "Projects",
                newName: "StoreManagerEmail");

            migrationBuilder.RenameColumn(
                name: "RecruiterMail",
                table: "Projects",
                newName: "RecruiterEmail");

            migrationBuilder.RenameColumn(
                name: "AreaManagerMail",
                table: "Projects",
                newName: "DirectorEmail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 19, 11, 47, 21, 864, DateTimeKind.Utc).AddTicks(2558),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 19, 11, 58, 27, 214, DateTimeKind.Utc).AddTicks(277));

            migrationBuilder.AddColumn<string>(
                name: "AreaManagerEmail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
