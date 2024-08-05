using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changedEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Positions_PositionId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Sections_SectionId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_SubSections_SubSectionId",
                table: "HeadCounts");

            migrationBuilder.AlterColumn<int>(
                name: "SubSectionId",
                table: "HeadCounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "HeadCounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "HeadCounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "HeadCounts",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Positions_PositionId",
                table: "HeadCounts",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Sections_SectionId",
                table: "HeadCounts",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_SubSections_SubSectionId",
                table: "HeadCounts",
                column: "SubSectionId",
                principalTable: "SubSections",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Positions_PositionId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_Sections_SectionId",
                table: "HeadCounts");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadCounts_SubSections_SubSectionId",
                table: "HeadCounts");

            migrationBuilder.AlterColumn<int>(
                name: "SubSectionId",
                table: "HeadCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "HeadCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "HeadCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "HeadCounts",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Employees_EmployeeId",
                table: "HeadCounts",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Positions_PositionId",
                table: "HeadCounts",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_Sections_SectionId",
                table: "HeadCounts",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadCounts_SubSections_SubSectionId",
                table: "HeadCounts",
                column: "SubSectionId",
                principalTable: "SubSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
