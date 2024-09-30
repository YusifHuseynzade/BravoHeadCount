using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedHeadCountHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 30, 9, 48, 45, 413, DateTimeKind.Utc).AddTicks(7409),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 19, 11, 58, 27, 214, DateTimeKind.Utc).AddTicks(277));

            migrationBuilder.AddColumn<DateTime>(
                name: "ChangeDate",
                table: "HeadCountHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "HeadCountHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FromProjectId",
                table: "HeadCountHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToProjectId",
                table: "HeadCountHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HeadCountHistories_EmployeeId",
                table: "HeadCountHistories",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadCountHistories_FromProjectId",
                table: "HeadCountHistories",
                column: "FromProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadCountHistories_ToProjectId",
                table: "HeadCountHistories",
                column: "ToProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCountHistories_Employees_EmployeeId",
                table: "HeadCountHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCountHistories_Projects_FromProjectId",
                table: "HeadCountHistories",
                column: "FromProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCountHistories_Projects_ToProjectId",
                table: "HeadCountHistories",
                column: "ToProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCountHistories_Employees_EmployeeId",
                table: "HeadCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCountHistories_Projects_FromProjectId",
                table: "HeadCountHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCountHistories_Projects_ToProjectId",
                table: "HeadCountHistories");

            migrationBuilder.DropIndex(
                name: "IX_HeadCountHistories_EmployeeId",
                table: "HeadCountHistories");

            migrationBuilder.DropIndex(
                name: "IX_HeadCountHistories_FromProjectId",
                table: "HeadCountHistories");

            migrationBuilder.DropIndex(
                name: "IX_HeadCountHistories_ToProjectId",
                table: "HeadCountHistories");

            migrationBuilder.DropColumn(
                name: "ChangeDate",
                table: "HeadCountHistories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "HeadCountHistories");

            migrationBuilder.DropColumn(
                name: "FromProjectId",
                table: "HeadCountHistories");

            migrationBuilder.DropColumn(
                name: "ToProjectId",
                table: "HeadCountHistories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 19, 11, 58, 27, 214, DateTimeKind.Utc).AddTicks(277),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 30, 9, 48, 45, 413, DateTimeKind.Utc).AddTicks(7409));
        }
    }
}
