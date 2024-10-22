using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class FactTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fact",
                table: "ScheduledDatas");

            migrationBuilder.AddColumn<int>(
                name: "FactId",
                table: "ScheduledDatas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledDatas_FactId",
                table: "ScheduledDatas",
                column: "FactId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledDatas_Facts_FactId",
                table: "ScheduledDatas",
                column: "FactId",
                principalTable: "Facts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledDatas_Facts_FactId",
                table: "ScheduledDatas");

            migrationBuilder.DropTable(
                name: "Facts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledDatas_FactId",
                table: "ScheduledDatas");

            migrationBuilder.DropColumn(
                name: "FactId",
                table: "ScheduledDatas");

            migrationBuilder.AddColumn<string>(
                name: "Fact",
                table: "ScheduledDatas",
                type: "text",
                nullable: true);
        }
    }
}
