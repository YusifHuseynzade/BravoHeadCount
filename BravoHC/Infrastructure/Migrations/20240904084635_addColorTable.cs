using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addColorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 4, 12, 46, 35, 593, DateTimeKind.Utc).AddTicks(5832),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 8, 13, 17, 10, 51, 353, DateTimeKind.Utc).AddTicks(1324));

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "HeadCounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "Employees",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FIN",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "HeadCountBackgroundColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColorHexCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadCountBackgroundColors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeadCounts_ColorId",
                table: "HeadCounts",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_HeadCountBackgroundColors_ColorId",
                table: "HeadCounts",
                column: "ColorId",
                principalTable: "HeadCountBackgroundColors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_HeadCountBackgroundColors_ColorId",
                table: "HeadCounts");

            migrationBuilder.DropTable(
                name: "HeadCountBackgroundColors");

            migrationBuilder.DropIndex(
                name: "IX_HeadCounts_ColorId",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FIN",
                table: "Employees");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 13, 17, 10, 51, 353, DateTimeKind.Utc).AddTicks(1324),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 4, 12, 46, 35, 593, DateTimeKind.Utc).AddTicks(5832));
        }
    }
}
