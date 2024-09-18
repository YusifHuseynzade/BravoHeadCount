using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddBakuDistrictMetroTargetTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                table: "ResidentalAreas");

            migrationBuilder.DropColumn(
                name: "Metro",
                table: "ResidentalAreas");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "ResidentalAreas");

            migrationBuilder.RenameColumn(
                name: "Director",
                table: "Projects",
                newName: "OperationDirectorMail");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 18, 46, 10, 978, DateTimeKind.Utc).AddTicks(7021),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 25, 25, 538, DateTimeKind.Utc).AddTicks(7125));

            migrationBuilder.AddColumn<string>(
                name: "AreaManagerBadge",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OperationDirector",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StoreClosedDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StoreOpeningDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BakuDistrictId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BakuMetroId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BakuTargetId",
                table: "Employees",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BakuDistricts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakuDistricts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakuMetros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakuMetros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BakuTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakuTargets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BakuDistrictId",
                table: "Employees",
                column: "BakuDistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BakuMetroId",
                table: "Employees",
                column: "BakuMetroId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BakuTargetId",
                table: "Employees",
                column: "BakuTargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BakuDistricts_BakuDistrictId",
                table: "Employees",
                column: "BakuDistrictId",
                principalTable: "BakuDistricts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BakuMetros_BakuMetroId",
                table: "Employees",
                column: "BakuMetroId",
                principalTable: "BakuMetros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BakuTargets_BakuTargetId",
                table: "Employees",
                column: "BakuTargetId",
                principalTable: "BakuTargets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BakuDistricts_BakuDistrictId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BakuMetros_BakuMetroId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BakuTargets_BakuTargetId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "BakuDistricts");

            migrationBuilder.DropTable(
                name: "BakuMetros");

            migrationBuilder.DropTable(
                name: "BakuTargets");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BakuDistrictId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BakuMetroId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BakuTargetId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AreaManagerBadge",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "OperationDirector",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StoreClosedDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StoreOpeningDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "BakuDistrictId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BakuMetroId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BakuTargetId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "OperationDirectorMail",
                table: "Projects",
                newName: "Director");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 25, 25, 538, DateTimeKind.Utc).AddTicks(7125),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 18, 46, 10, 978, DateTimeKind.Utc).AddTicks(7021));

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "ResidentalAreas",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Metro",
                table: "ResidentalAreas",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "ResidentalAreas",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");
        }
    }
}
