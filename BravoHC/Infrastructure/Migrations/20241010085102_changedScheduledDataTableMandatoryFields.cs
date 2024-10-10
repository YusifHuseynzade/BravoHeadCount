using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changedScheduledDataTableMandatoryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "ScheduledDatas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas");

            migrationBuilder.AlterColumn<int>(
                name: "PlanId",
                table: "ScheduledDatas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledDatas_Plans_PlanId",
                table: "ScheduledDatas",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
