using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedEmployeeAndStoreTableRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Projects_ProjectId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Employees_StoreId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Stores",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "AreaManagerId",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecruiterId",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreManagerId",
                table: "Stores",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AreaManagerId",
                table: "Stores",
                column: "AreaManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DirectorId",
                table: "Stores",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_RecruiterId",
                table: "Stores",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreManagerId",
                table: "Stores",
                column: "StoreManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_AreaManagerId",
                table: "Stores",
                column: "AreaManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_DirectorId",
                table: "Stores",
                column: "DirectorId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_RecruiterId",
                table: "Stores",
                column: "RecruiterId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Employees_StoreManagerId",
                table: "Stores",
                column: "StoreManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Projects_ProjectId",
                table: "Stores",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_AreaManagerId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_DirectorId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_RecruiterId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Employees_StoreManagerId",
                table: "Stores");

            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Projects_ProjectId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AreaManagerId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DirectorId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_RecruiterId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_StoreManagerId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "AreaManagerId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreManagerId",
                table: "Stores");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_StoreId",
                table: "Employees",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Stores_StoreId",
                table: "Employees",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Projects_ProjectId",
                table: "Stores",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
