using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ScheduledDataAndOtherDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraduationSchedule",
                table: "ScheduledDatas");

            migrationBuilder.DropColumn(
                name: "Plan",
                table: "ScheduledDatas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 10, 7, 16, 12, 35, 434, DateTimeKind.Utc).AddTicks(6397));

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "ScheduledDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VacationScheduleId",
                table: "ScheduledDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Shift = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SickLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SickLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SickLeaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationSchedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    MonthId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    WorkdaysCount = table.Column<int>(type: "integer", nullable: false),
                    VacationDaysCount = table.Column<int>(type: "integer", nullable: false),
                    SickDaysCount = table.Column<int>(type: "integer", nullable: false),
                    DayOffCount = table.Column<int>(type: "integer", nullable: false),
                    AbsentDaysCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Summaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Summaries_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDatas_PlanId",
                table: "ScheduledDatas",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDatas_VacationScheduleId",
                table: "ScheduledDatas",
                column: "VacationScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_SickLeaves_EmployeeId",
                table: "SickLeaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_EmployeeId",
                table: "Summaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_MonthId",
                table: "Summaries",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationSchedules_EmployeeId",
                table: "VacationSchedules",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledDatas_VacationSchedules_VacationScheduleId",
                table: "ScheduledDatas",
                column: "VacationScheduleId",
                principalTable: "VacationSchedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledDatas_VacationSchedules_VacationScheduleId",
                table: "ScheduledDatas");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "SickLeaves");

            migrationBuilder.DropTable(
                name: "Summaries");

            migrationBuilder.DropTable(
                name: "VacationSchedules");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledDatas_PlanId",
                table: "ScheduledDatas");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledDatas_VacationScheduleId",
                table: "ScheduledDatas");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "ScheduledDatas");

            migrationBuilder.DropColumn(
                name: "VacationScheduleId",
                table: "ScheduledDatas");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 10, 7, 16, 12, 35, 434, DateTimeKind.Utc).AddTicks(6397),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "GraduationSchedule",
                table: "ScheduledDatas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Plan",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
