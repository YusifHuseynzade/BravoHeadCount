using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateProjectTableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects");

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
                name: "FK_Stores_FunctionalAreas_FunctionalAreaId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_AreaManagerId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_DirectorId",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_FunctionalAreaId",
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
                name: "FunctionalAreaId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "RecruiterId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "StoreManagerId",
                table: "Stores");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 13, 17, 16, 465, DateTimeKind.Utc).AddTicks(8552),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 1, 54, 24, 54, DateTimeKind.Utc).AddTicks(4325));

            migrationBuilder.AlterColumn<int>(
                name: "FunctionalAreaId",
                table: "Projects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "AreaManager",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AreaManagerEmail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DirectorEmail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FunctionalArea",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Projects",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recruiter",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RecruiterEmail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoreManagerEmail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProjectHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NewAreaManager",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewAreaManagerEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewDirector",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewDirectorEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewFormat",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewFunctionalArea",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "NewIsActive",
                table: "ProjectHistories",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewRecruiter",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewRecruiterEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewStoreManagerEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldAreaManager",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldAreaManagerEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldDirector",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldDirectorEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldFormat",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldFunctionalArea",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "OldIsActive",
                table: "ProjectHistories",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldRecruiter",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldRecruiterEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldStoreManagerEmail",
                table: "ProjectHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ProjectHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistories_ProjectId",
                table: "ProjectHistories",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistories_Projects_ProjectId",
                table: "ProjectHistories",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistories_Projects_ProjectId",
                table: "ProjectHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHistories_ProjectId",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "AreaManager",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AreaManagerEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DirectorEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FunctionalArea",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Recruiter",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RecruiterEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StoreManagerEmail",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewAreaManager",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewAreaManagerEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewDirector",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewDirectorEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewFormat",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewFunctionalArea",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewIsActive",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewRecruiter",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewRecruiterEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "NewStoreManagerEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldAreaManager",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldAreaManagerEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldDirector",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldDirectorEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldFormat",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldFunctionalArea",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldIsActive",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldRecruiter",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldRecruiterEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OldStoreManagerEmail",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectHistories");

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
                name: "FunctionalAreaId",
                table: "Stores",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "ScheduledDatas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 18, 1, 54, 24, 54, DateTimeKind.Utc).AddTicks(4325),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 9, 18, 13, 17, 16, 465, DateTimeKind.Utc).AddTicks(8552));

            migrationBuilder.AlterColumn<int>(
                name: "FunctionalAreaId",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AreaManagerId",
                table: "Stores",
                column: "AreaManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DirectorId",
                table: "Stores",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_FunctionalAreaId",
                table: "Stores",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_RecruiterId",
                table: "Stores",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_StoreManagerId",
                table: "Stores",
                column: "StoreManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_FunctionalAreas_FunctionalAreaId",
                table: "Projects",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Stores_FunctionalAreas_FunctionalAreaId",
                table: "Stores",
                column: "FunctionalAreaId",
                principalTable: "FunctionalAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
