using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedRoleTableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 1, 54, 24, 54, DateTimeKind.Utc).AddTicks(4325),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 1, 39, 43, 379, DateTimeKind.Utc).AddTicks(5378));

            migrationBuilder.AddColumn<string>(
                name: "RoleLevel",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleType",
                table: "Roles",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleLevel",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "Roles");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 1, 39, 43, 379, DateTimeKind.Utc).AddTicks(5378),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 1, 54, 24, 54, DateTimeKind.Utc).AddTicks(4325));
        }
    }
}
