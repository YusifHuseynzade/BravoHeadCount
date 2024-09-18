using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateResidanetalAreaTableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FunctionalAreas_FunctionalAreaId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_FunctionalAreas_FunctionalAreaId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Formats_FormatId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "Formats");

            migrationBuilder.DropTable(
                name: "FunctionalAreas");

            migrationBuilder.DropIndex(
                name: "IX_Stores_FormatId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Projects_FunctionalAreaId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_HeadCounts_FunctionalAreaId",
                table: "HeadCounts");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FunctionalAreaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FormatId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "FunctionalAreaId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FunctionalAreaId",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "RecruiterComment",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "FunctionalAreaId",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 16, 25, 25, 538, DateTimeKind.Utc).AddTicks(7125),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 21, 3, 136, DateTimeKind.Utc).AddTicks(1583));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentalAreas",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

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

            migrationBuilder.AddColumn<string>(
                name: "RecruiterComment",
                table: "Employees",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "RecruiterComment",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "FormatId",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 21, 3, 136, DateTimeKind.Utc).AddTicks(1583),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 16, 25, 25, 538, DateTimeKind.Utc).AddTicks(7125));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ResidentalAreas",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(35)",
                oldMaxLength: 35);

            migrationBuilder.AddColumn<int>(
                name: "FunctionalAreaId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FunctionalAreaId",
                table: "HeadCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RecruiterComment",
                table: "HeadCounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FunctionalAreaId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalAreas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FormatId",
                table: "Stores",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FunctionalAreaId",
                table: "Projects",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadCounts_FunctionalAreaId",
                table: "HeadCounts",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FunctionalAreaId",
                table: "Employees",
                column: "FunctionalAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FunctionalAreas_FunctionalAreaId",
                table: "Employees",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_FunctionalAreas_FunctionalAreaId",
                table: "HeadCounts",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Formats_FormatId",
                table: "Stores",
                column: "FormatId",
                principalTable: "Formats",
                principalColumn: "Id");
        }
    }
}
