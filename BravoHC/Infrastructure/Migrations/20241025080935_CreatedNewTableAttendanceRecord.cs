using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CreatedNewTableAttendanceRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectNameForBank",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StoreUserMail",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<string>(type: "text", nullable: false),
                    AccessDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccessDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    PersonName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EndOfMonthReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    EncashmentAmount = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    DepositAmount = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    PettyCashAmount = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfMonthReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndOfMonthReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpensesReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UtilityElectricity = table.Column<float>(type: "real", nullable: false),
                    UtilityWater = table.Column<float>(type: "real", nullable: false),
                    RepairExpenses = table.Column<float>(type: "real", nullable: false),
                    TransportationExpenses = table.Column<float>(type: "real", nullable: false),
                    CleaningExpenses = table.Column<float>(type: "real", nullable: false),
                    StationeryExpenses = table.Column<float>(type: "real", nullable: false),
                    PrintingExpenses = table.Column<float>(type: "real", nullable: false),
                    OperationExpenses = table.Column<float>(type: "real", nullable: false),
                    Other = table.Column<float>(type: "real", nullable: false),
                    TotalExpenses = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpensesReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpensesReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encashments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    AmountFromSales = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    AmountFoundOnSite = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    SafeSurplus = table.Column<float>(type: "numeric(18,2)", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    SealNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encashments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encashments_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encashments_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoneyOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
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
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    SealNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyOrders_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoneyOrders_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SettingFinanceOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EncashmentDays = table.Column<string>(type: "text", nullable: false),
                    DateEncashment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActiveEncashment = table.Column<bool>(type: "boolean", nullable: false),
                    FrequencyEncashment = table.Column<int>(type: "integer", nullable: false),
                    EncashmentRecipient = table.Column<List<string>>(type: "text[]", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    MoneyOrderDays = table.Column<string>(type: "text", nullable: false),
                    DateMoneyOrder = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActiveMoneyOrder = table.Column<bool>(type: "boolean", nullable: false),
                    FrequencyMoneyOrder = table.Column<int>(type: "integer", nullable: false),
                    MoneyOrderRecipient = table.Column<List<string>>(type: "text[]", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BranchId = table.Column<int>(type: "integer", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingFinanceOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingFinanceOperations_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingFinanceOperations_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExpensesReportId = table.Column<int>(type: "integer", nullable: false),
                    EncashmentId = table.Column<int>(type: "integer", nullable: false),
                    UploadedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UploadedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Encashments_EncashmentId",
                        column: x => x.EncashmentId,
                        principalTable: "Encashments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachments_ExpensesReports_ExpensesReportId",
                        column: x => x.ExpensesReportId,
                        principalTable: "ExpensesReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EncashmentId",
                table: "Attachments",
                column: "EncashmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ExpensesReportId",
                table: "Attachments",
                column: "ExpensesReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Encashments_BranchId",
                table: "Encashments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Encashments_ProjectId",
                table: "Encashments",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfMonthReports_ProjectId",
                table: "EndOfMonthReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpensesReports_ProjectId",
                table: "ExpensesReports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOrders_BranchId",
                table: "MoneyOrders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOrders_ProjectId",
                table: "MoneyOrders",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingFinanceOperations_BranchId",
                table: "SettingFinanceOperations",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingFinanceOperations_ProjectId",
                table: "SettingFinanceOperations",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "AttendanceRecords");

            migrationBuilder.DropTable(
                name: "EndOfMonthReports");

            migrationBuilder.DropTable(
                name: "MoneyOrders");

            migrationBuilder.DropTable(
                name: "SettingFinanceOperations");

            migrationBuilder.DropTable(
                name: "Encashments");

            migrationBuilder.DropTable(
                name: "ExpensesReports");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropColumn(
                name: "ProjectNameForBank",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StoreUserMail",
                table: "Projects");
        }
    }
}
