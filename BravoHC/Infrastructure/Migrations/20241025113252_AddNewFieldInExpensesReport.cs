using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddNewFieldInExpensesReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PettyCashAmount",
                table: "EndOfMonthReports");

            migrationBuilder.AddColumn<float>(
                name: "BalanceEndMonth",
                table: "ExpensesReports",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceEndMonth",
                table: "ExpensesReports");

            migrationBuilder.AddColumn<float>(
                name: "PettyCashAmount",
                table: "EndOfMonthReports",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
