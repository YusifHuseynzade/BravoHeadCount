using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddNewHistoryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncashmentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EncashmentId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    AmountFromSales = table.Column<float>(type: "real", nullable: false),
                    AmountFoundOnSite = table.Column<float>(type: "real", nullable: false),
                    SafeSurplus = table.Column<float>(type: "real", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    SealNumber = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncashmentHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndOfMonthReportHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndOfMonthReportId = table.Column<int>(type: "integer", nullable: false),
                    EncashmentAmount = table.Column<float>(type: "real", nullable: false),
                    DepositAmount = table.Column<float>(type: "real", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfMonthReportHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesReportHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpensesReportId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    UtilityElectricity = table.Column<float>(type: "real", nullable: false),
                    UtilityWater = table.Column<float>(type: "real", nullable: false),
                    RepairExpenses = table.Column<float>(type: "real", nullable: false),
                    TransportationExpenses = table.Column<float>(type: "real", nullable: false),
                    CleaningExpenses = table.Column<float>(type: "real", nullable: false),
                    StationeryExpenses = table.Column<float>(type: "real", nullable: false),
                    PrintingExpenses = table.Column<float>(type: "real", nullable: false),
                    OperationExpenses = table.Column<float>(type: "real", nullable: false),
                    BalanceEndMonth = table.Column<float>(type: "real", nullable: false),
                    Other = table.Column<float>(type: "real", nullable: false),
                    TotalExpenses = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesReportHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyOrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MoneyOrderId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    HundredAZN = table.Column<int>(type: "integer", nullable: false),
                    FiftyAZN = table.Column<float>(type: "real", nullable: false),
                    TwentyAZN = table.Column<float>(type: "real", nullable: false),
                    TenAZN = table.Column<float>(type: "real", nullable: false),
                    FiveAZN = table.Column<float>(type: "real", nullable: false),
                    OneAZN = table.Column<float>(type: "real", nullable: false),
                    FiftyQapik = table.Column<float>(type: "real", nullable: false),
                    TwentyQapik = table.Column<float>(type: "real", nullable: false),
                    TenQapik = table.Column<float>(type: "real", nullable: false),
                    FiveQapik = table.Column<float>(type: "real", nullable: false),
                    ThreeQapik = table.Column<float>(type: "real", nullable: false),
                    OneQapik = table.Column<float>(type: "real", nullable: false),
                    TotalQuantity = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOrderHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrolleyHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TrolleyId = table.Column<int>(type: "integer", nullable: false),
                    WorkingTrolleysCount = table.Column<int>(type: "integer", nullable: false),
                    BrokenTrolleysCount = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrolleyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trolleys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    TrolleyTypeId = table.Column<int>(type: "integer", nullable: false),
                    CountDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WorkingTrolleysCount = table.Column<int>(type: "integer", nullable: false),
                    BrokenTrolleysCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trolleys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trolleys_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trolleys_TrolleyTypes_TrolleyTypeId",
                        column: x => x.TrolleyTypeId,
                        principalTable: "TrolleyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trolleys_ProjectId",
                table: "Trolleys",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Trolleys_TrolleyTypeId",
                table: "Trolleys",
                column: "TrolleyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncashmentHistories");

            migrationBuilder.DropTable(
                name: "EndOfMonthReportHistories");

            migrationBuilder.DropTable(
                name: "ExpensesReportHistories");

            migrationBuilder.DropTable(
                name: "MoneyOrderHistories");

            migrationBuilder.DropTable(
                name: "TrolleyHistories");

            migrationBuilder.DropTable(
                name: "Trolleys");

            migrationBuilder.DropTable(
                name: "TrolleyTypes");
        }
    }
}
