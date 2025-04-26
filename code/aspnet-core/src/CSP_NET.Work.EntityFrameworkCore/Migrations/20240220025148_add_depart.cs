using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class adddepart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DEPARTMENT",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "WMS_PROJECT_GENERAL",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<Guid>(
                name: "DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WMS_PROJECT_GENERAL_DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL",
                column: "DEPARTMENTID");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_AbpOrganizationUnits_DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL",
                column: "DEPARTMENTID",
                principalTable: "AbpOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_AbpOrganizationUnits_DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.DropIndex(
                name: "IX_WMS_PROJECT_GENERAL_DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.DropColumn(
                name: "DEPARTMENTID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AlterColumn<string>(
                name: "DESCRIPTION",
                table: "WMS_PROJECT_GENERAL",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DEPARTMENT",
                table: "WMS_PROJECT_GENERAL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
