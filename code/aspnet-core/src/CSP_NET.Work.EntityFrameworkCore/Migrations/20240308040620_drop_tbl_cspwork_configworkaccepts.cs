using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class droptblcspworkconfigworkaccepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_CONFIGWORK_ACCEPTS");

            migrationBuilder.DropTable(
                name: "WMS_CspWorks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WMS_CONFIGWORK_ACCEPTS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ACCEPT2 = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DATEACCEPT1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DATEACCEPT2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    STATUSACCEPT1 = table.Column<short>(type: "smallint", nullable: false),
                    STATUSACCEPT2 = table.Column<short>(type: "smallint", nullable: false),
                    USERACCEPT1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USERACCEP2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WORKID = table.Column<Guid>(name: "WORK_ID", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523809", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WMS_CspWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PARENTID = table.Column<Guid>(name: "PARENT_ID", type: "uniqueidentifier", nullable: true),
                    PROJECTID = table.Column<Guid>(name: "PROJECT_ID", type: "uniqueidentifier", nullable: false),
                    ASSIGNER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ASSIGNERNAME = table.Column<string>(name: "ASSIGNER_NAME", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    COLLABRATORNAMES = table.Column<string>(name: "COLLABRATOR_NAMES", type: "nvarchar(max)", nullable: true),
                    COLLABRATORS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMPLEXITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DEGREEOFIMPORTANT = table.Column<string>(name: "DEGREE_OF_IMPORTANT", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DUEDATE = table.Column<DateTime>(name: "DUE_DATE", type: "datetime2", nullable: true),
                    DURATION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ESTIMATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FULLNAME = table.Column<string>(name: "FULL_NAME", type: "nvarchar(max)", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LEVEL = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MEMBERS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NOMALIZEDNAME = table.Column<string>(name: "NOMALIZED_NAME", type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NOMALIZEDTASKCODE = table.Column<string>(name: "NOMALIZED_TASK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NOMALIZEDWORKCODE = table.Column<string>(name: "NOMALIZED_WORK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    OWNER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OWNERName = table.Column<string>(name: "OWNER_Name", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PERFORMER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PERFORMERNAME = table.Column<string>(name: "PERFORMER_NAME", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PRIORITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PROGRESS = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    PROJECTNAME = table.Column<string>(name: "PROJECT_NAME", type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RESTRICTCOMPLETE = table.Column<bool>(name: "RESTRICT_COMPLETE", type: "bit", nullable: false, defaultValue: false),
                    STARTDATE = table.Column<DateTime>(name: "START_DATE", type: "datetime2", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    TASKCODE = table.Column<string>(name: "TASK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    WORKCODE = table.Column<string>(name: "WORK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CheckList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WMS_CspWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WMS_CspWorks_WMS_CspWorks_PARENT_ID",
                        column: x => x.PARENTID,
                        principalTable: "WMS_CspWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WMS_CspWorks_WMS_PROJECT_GENERAL_PROJECT_ID",
                        column: x => x.PROJECTID,
                        principalTable: "WMS_PROJECT_GENERAL",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_CODE",
                table: "WMS_CspWorks",
                column: "CODE");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_NAME",
                table: "WMS_CspWorks",
                column: "NAME");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_NOMALIZED_NAME",
                table: "WMS_CspWorks",
                column: "NOMALIZED_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_NOMALIZED_TASK_CODE",
                table: "WMS_CspWorks",
                column: "NOMALIZED_TASK_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_NOMALIZED_WORK_CODE",
                table: "WMS_CspWorks",
                column: "NOMALIZED_WORK_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_PARENT_ID",
                table: "WMS_CspWorks",
                column: "PARENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_PRIORITY",
                table: "WMS_CspWorks",
                column: "PRIORITY");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_PROJECT_ID",
                table: "WMS_CspWorks",
                column: "PROJECT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_STATUS",
                table: "WMS_CspWorks",
                column: "STATUS");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_TASK_CODE",
                table: "WMS_CspWorks",
                column: "TASK_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_CspWorks_WORK_CODE",
                table: "WMS_CspWorks",
                column: "WORK_CODE");
        }
    }
}
