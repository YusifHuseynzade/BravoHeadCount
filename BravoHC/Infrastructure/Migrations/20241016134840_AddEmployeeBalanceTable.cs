using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddEmployeeBalanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduationBalance",
                table: "ScheduledDatas");

            migrationBuilder.DropColumn(
                name: "HolidayBalance",
                table: "ScheduledDatas");

            migrationBuilder.CreateTable(
                name: "EmployeeBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    HolidayBalance = table.Column<int>(type: "integer", nullable: false),
                    VacationBalance = table.Column<int>(type: "integer", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBalances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBalances_EmployeeId",
                table: "EmployeeBalances",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBalances");

            migrationBuilder.AddColumn<int>(
                name: "GraduationBalance",
                table: "ScheduledDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HolidayBalance",
                table: "ScheduledDatas",
                type: "integer",
                nullable: true);
        }
    }
}
