using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class droptblconfigWorkAccepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_CONFIGWORK_ACCEPTS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WMS_CONFIGWORK_ACCEPTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USERACCEPT1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USERACCEP2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Accept2 = table.Column<bool>(type: "bit", nullable: false),
                    DATEACCEPT1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATEACCEPT2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STATUSACCEPT1 = table.Column<short>(type: "smallint", nullable: false),
                    STATUSACCEPT2 = table.Column<short>(type: "smallint", nullable: false),
                    WORKID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523809", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CONFIGWORK_ACCEPT_USER1",
                        column: x => x.USERACCEPT1,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CONFIGWORK_ACCEPT_USER2",
                        column: x => x.USERACCEP2,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CONFIGWORK_ACCEPTS_USERACCEP2",
                table: "WMS_CONFIGWORK_ACCEPTS",
                column: "USERACCEP2");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CONFIGWORK_ACCEPTS_USERACCEPT1",
                table: "WMS_CONFIGWORK_ACCEPTS",
                column: "USERACCEPT1");
        }
    }
}
