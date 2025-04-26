using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class addfixProjectSateID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AlterColumn<Guid>(
                name: "PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                column: "PROJECTSTATEID",
                principalTable: "WMS_PROJECT_STATES",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL");

            migrationBuilder.AlterColumn<Guid>(
                name: "PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_PROJECT_GENERAL_WMS_PROJECT_STATES_PROJECTSTATEID",
                table: "WMS_PROJECT_GENERAL",
                column: "PROJECTSTATEID",
                principalTable: "WMS_PROJECT_STATES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
