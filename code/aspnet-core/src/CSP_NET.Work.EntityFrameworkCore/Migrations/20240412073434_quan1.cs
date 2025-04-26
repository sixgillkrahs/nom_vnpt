using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class quan1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "WMS_Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    WORKID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEBY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FILESIZE = table.Column<long>(type: "bigint", nullable: false),
                    MIMETYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523812", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WMS_Document_WMS_CspWorks_WORKID",
                        column: x => x.WORKID,
                        principalTable: "WMS_CspWorks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_Document_WORKID",
                table: "WMS_Document",
                column: "WORKID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_Document");

            migrationBuilder.CreateTable(
                name: "WMS_ResourseManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WORKID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FORMAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATEBY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPDATEDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523812", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WMS_ResourseManagement_WMS_CspWorks_WORKID",
                        column: x => x.WORKID,
                        principalTable: "WMS_CspWorks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_ResourseManagement_WORKID",
                table: "WMS_ResourseManagement",
                column: "WORKID");
        }
    }
}
