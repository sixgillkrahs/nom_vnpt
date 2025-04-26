using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class CREATETBLCONFIGwORKaCCEPTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WMS_CONFIGWORK_ACCEPTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WORKID = table.Column<Guid>(name: "WORK_ID", type: "uniqueidentifier", nullable: false),
                    USERACCEPT1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DATEACCEPT1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    USERACCEP2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DATEACCEPT2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUSACCEPT1 = table.Column<short>(type: "smallint", nullable: false),
                    STATUSACCEPT2 = table.Column<short>(type: "smallint", nullable: false),
                    ACCEPT2 = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523809", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WMS_CONFIGWORK_ACCEPTS_WMS_CspWorks_WORK_ID",
                        column: x => x.WORKID,
                        principalTable: "WMS_CspWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CONFIGWORK_ACCEPTS_WORK_ID",
                table: "WMS_CONFIGWORK_ACCEPTS",
                column: "WORK_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_CONFIGWORK_ACCEPTS");
        }
    }
}
