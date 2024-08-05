using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changedEmployeeTableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAreaManager",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDirector",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsRecruiter",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsStoreManager",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "AreaManager",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recruiter",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoreManager",
                table: "Employees",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaManager",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Recruiter",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StoreManager",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "IsAreaManager",
                table: "Employees",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDirector",
                table: "Employees",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecruiter",
                table: "Employees",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsStoreManager",
                table: "Employees",
                type: "boolean",
                nullable: true);
        }
    }
}
