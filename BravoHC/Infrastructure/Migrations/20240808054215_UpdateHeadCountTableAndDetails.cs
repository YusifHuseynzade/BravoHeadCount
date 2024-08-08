using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateHeadCountTableAndDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsStore",
                table: "Projects",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsHeadOffice",
                table: "Projects",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<bool>(
                name: "IsVacant",
                table: "HeadCounts",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "HeadCounts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecruiterComment",
                table: "HeadCounts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeadCounts_ParentId",
                table: "HeadCounts",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_HeadCounts_ParentId",
                table: "HeadCounts",
                column: "ParentId",
                principalTable: "HeadCounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_HeadCounts_ParentId",
                table: "HeadCounts");

            migrationBuilder.DropIndex(
                name: "IX_HeadCounts_ParentId",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "IsVacant",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "HeadCounts");

            migrationBuilder.DropColumn(
                name: "RecruiterComment",
                table: "HeadCounts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsStore",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsHeadOffice",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
