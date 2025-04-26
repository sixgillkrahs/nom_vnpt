using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class changefkworkapproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CONFIGWORK_ACCEPTS_WMS_CspWorks_WORK_ID",
                table: "WMS_CONFIGWORK_ACCEPTS");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CONFIGWORK_ACCEPTS_WMS_CspWorks_WORK_ID",
                table: "WMS_CONFIGWORK_ACCEPTS",
                column: "WORK_ID",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WMS_CONFIGWORK_ACCEPTS_WMS_CspWorks_WORK_ID",
                table: "WMS_CONFIGWORK_ACCEPTS");

            migrationBuilder.AddForeignKey(
                name: "FK_WMS_CONFIGWORK_ACCEPTS_WMS_CspWorks_WORK_ID",
                table: "WMS_CONFIGWORK_ACCEPTS",
                column: "WORK_ID",
                principalTable: "WMS_CspWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
