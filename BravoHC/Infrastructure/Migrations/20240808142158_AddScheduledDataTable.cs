using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddScheduledDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduledDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 8, 8, 18, 21, 58, 857, DateTimeKind.Utc).AddTicks(2585)),
                    Plan = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fact = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GraduationSchedule = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    HolidayBalance = table.Column<int>(type: "integer", nullable: false),
                    GraduationBalance = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledDatas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScheduledDatas_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDatas_EmployeeId",
                table: "ScheduledDatas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDatas_ProjectId",
                table: "ScheduledDatas",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduledDatas");
        }
    }
}
