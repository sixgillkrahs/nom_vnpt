using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class fkworklogwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CspWorks_WMS_CspWorks_PARENT_ID",
                table: "WMS_CspWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CspWorks_WMS_PROJECT_GENERAL_PROJECT_ID",
                table: "WMS_CspWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_WMS_LogWork_WMS_CspWorks_CspWorkId",
                table: "WMS_LogWork");

            migrationBuilder.DropIndex(
                name: "IX_WMS_LogWork_CspWorkId",
                table: "WMS_LogWork");

            migrationBuilder.DropColumn(
                name: "CspWorkId",
                table: "WMS_LogWork");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_LogWork_WORKID",
                table: "WMS_LogWork",
                column: "WORKID");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CspWorks_WMS_CspWorks_PARENT_ID",
                table: "WMS_CspWorks",
                column: "PARENT_ID",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CspWorks_WMS_PROJECT_GENERAL_PROJECT_ID",
                table: "WMS_CspWorks",
                column: "PROJECT_ID",
                principalTable: "WMS_PROJECT_GENERAL",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_LogWork_WMS_CspWorks_WORKID",
                table: "WMS_LogWork",
                column: "WORKID",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CspWorks_WMS_CspWorks_PARENT_ID",
                table: "WMS_CspWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CspWorks_WMS_PROJECT_GENERAL_PROJECT_ID",
                table: "WMS_CspWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_WMS_LogWork_WMS_CspWorks_WORKID",
                table: "WMS_LogWork");

            migrationBuilder.DropIndex(
                name: "IX_WMS_LogWork_WORKID",
                table: "WMS_LogWork");

            migrationBuilder.AddColumn<Guid>(
                name: "CspWorkId",
                table: "WMS_LogWork",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WMS_LogWork_CspWorkId",
                table: "WMS_LogWork",
                column: "CspWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CspWorks_WMS_CspWorks_PARENT_ID",
                table: "WMS_CspWorks",
                column: "PARENT_ID",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CspWorks_WMS_PROJECT_GENERAL_PROJECT_ID",
                table: "WMS_CspWorks",
                column: "PROJECT_ID",
                principalTable: "WMS_PROJECT_GENERAL",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_LogWork_WMS_CspWorks_CspWorkId",
                table: "WMS_LogWork",
                column: "CspWorkId",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
