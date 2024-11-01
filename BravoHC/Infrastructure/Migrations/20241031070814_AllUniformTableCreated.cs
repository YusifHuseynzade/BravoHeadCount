using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AllUniformTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PantSize",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShirtSize",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UniformConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    FunctionalArea = table.Column<string>(type: "text", nullable: false),
                    UniName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    CountUniform = table.Column<int>(type: "integer", nullable: false),
                    UniType = table.Column<int>(type: "integer", nullable: false),
                    UsageDuration = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniformConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniformConditions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uniforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniCode = table.Column<string>(type: "text", nullable: false),
                    UniName = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    UniType = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    UsageDuration = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uniforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BGSStockRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UniformId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: true),
                    RequestCount = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BGSStockRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BGSStockRequests_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BGSStockRequests_Uniforms_UniformId",
                        column: x => x.UniformId,
                        principalTable: "Uniforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DCStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockCount = table.Column<int>(type: "integer", nullable: false),
                    ImportedStockCount = table.Column<int>(type: "integer", nullable: false),
                    StoreOrUser = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    UniformId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: false),
                    ReceptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DCStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DCStocks_Uniforms_UniformId",
                        column: x => x.UniformId,
                        principalTable: "Uniforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreStockRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    UniformId = table.Column<int>(type: "integer", nullable: false),
                    RequestCount = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreStockRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreStockRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreStockRequests_Uniforms_UniformId",
                        column: x => x.UniformId,
                        principalTable: "Uniforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    UniformId = table.Column<int>(type: "integer", nullable: false),
                    UniCount = table.Column<int>(type: "integer", nullable: false),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    SenderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Receiver = table.Column<string>(type: "text", nullable: true),
                    AcceptedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DCStockIds = table.Column<List<int>>(type: "integer[]", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsBBgs = table.Column<bool>(type: "boolean", nullable: false),
                    IsFirstDistribution = table.Column<bool>(type: "boolean", nullable: false),
                    IsEnacted = table.Column<bool>(type: "boolean", nullable: true),
                    EnactedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PayrollStatus = table.Column<bool>(type: "boolean", nullable: true),
                    HandoveredBy = table.Column<string>(type: "text", nullable: true),
                    DeductedBy = table.Column<string>(type: "text", nullable: true),
                    DeductedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TransactionStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Uniforms_UniformId",
                        column: x => x.UniformId,
                        principalTable: "Uniforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BGSStockRequests_ProjectId",
                table: "BGSStockRequests",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BGSStockRequests_UniformId",
                table: "BGSStockRequests",
                column: "UniformId");

            migrationBuilder.CreateIndex(
                name: "IX_DCStocks_UniformId",
                table: "DCStocks",
                column: "UniformId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockRequests_EmployeeId",
                table: "StoreStockRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreStockRequests_UniformId",
                table: "StoreStockRequests",
                column: "UniformId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_EmployeeId",
                table: "Transactions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ProjectId",
                table: "Transactions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UniformId",
                table: "Transactions",
                column: "UniformId");

            migrationBuilder.CreateIndex(
                name: "IX_UniformConditions_PositionId",
                table: "UniformConditions",
                column: "PositionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BGSStockRequests");

            migrationBuilder.DropTable(
                name: "DCStocks");

            migrationBuilder.DropTable(
                name: "StoreStockRequests");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "UniformConditions");

            migrationBuilder.DropTable(
                name: "Uniforms");

            migrationBuilder.DropColumn(
                name: "PantSize",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ShirtSize",
                table: "Employees");
        }
    }
}
