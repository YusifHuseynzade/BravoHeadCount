using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedEmployeeConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 6, 22, 7, 51, 571, DateTimeKind.Utc).AddTicks(9699),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 9, 48, 45, 413, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 9, 48, 45, 413, DateTimeKind.Utc).AddTicks(7409),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 6, 22, 7, 51, 571, DateTimeKind.Utc).AddTicks(9699));

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
