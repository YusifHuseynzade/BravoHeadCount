using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class EmployeeTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 11, 16, 57, 47, 160, DateTimeKind.Utc).AddTicks(3876),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 12, 46, 35, 593, DateTimeKind.Utc).AddTicks(5832));

            migrationBuilder.AlterColumn<string>(
                name: "ContractEndDate",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 12, 46, 35, 593, DateTimeKind.Utc).AddTicks(5832),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 11, 16, 57, 47, 160, DateTimeKind.Utc).AddTicks(3876));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ContractEndDate",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
