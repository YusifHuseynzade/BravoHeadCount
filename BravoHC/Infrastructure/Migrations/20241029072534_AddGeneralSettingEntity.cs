using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddGeneralSettingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CountDate",
                table: "TrolleyHistories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GeneralSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndOfMonthSendingTimes = table.Column<string>(type: "text", nullable: false),
                    EndOfMonthSendingFrequency = table.Column<int>(type: "integer", nullable: false),
                    EndOfMonthReceivers = table.Column<string>(type: "text", nullable: false),
                    EndOfMonthReceiversCC = table.Column<string>(type: "text", nullable: false),
                    EndOfMonthAvailableCreatedDays = table.Column<string>(type: "text", nullable: false),
                    ExpenseSendingTimes = table.Column<string>(type: "text", nullable: false),
                    ExpenseSendingFrequency = table.Column<int>(type: "integer", nullable: false),
                    ExpenseReceivers = table.Column<string>(type: "text", nullable: false),
                    ExpenseReceiversCC = table.Column<string>(type: "text", nullable: false),
                    ExpenseAvailableCreatedDays = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralSettings");

            migrationBuilder.DropColumn(
                name: "CountDate",
                table: "TrolleyHistories");
        }
    }
}
