using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class addState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STATE",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AddColumn<Guid>(
                name: "PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WMS_PROJECT_GENERAL_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                column: "PROJECTSTATEID");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                column: "PROJECTSTATEID",
                principalTable: "WMS_PROJECT_STATES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.DropIndex(
                name: "IX_WMS_PROJECT_GENERAL_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.DropColumn(
                name: "PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AddColumn<string>(
                name: "STATE",
                table: "WMS_PROJECT_GENERAL",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
