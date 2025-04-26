using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class createtblCspWorks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WMS_CspWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WORKCODE = table.Column<string>(name: "WORK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NOMALIZEDWORKCODE = table.Column<string>(name: "NOMALIZED_WORK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TASKCODE = table.Column<string>(name: "TASK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    NOMALIZEDTASKCODE = table.Column<string>(name: "NOMALIZED_TASK_CODE", type: "nvarchar(128)", maxLength: 128, nullable: false),
                    STARTDATE = table.Column<DateTime>(name: "START_DATE", type: "datetime2", nullable: true),
                    ESTIMATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DUEDATE = table.Column<DateTime>(name: "DUE_DATE", type: "datetime2", nullable: true),
                    PROGRESS = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    DURATION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PRIORITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    COMPLEXITY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DEGREEOFIMPORTANT = table.Column<string>(name: "DEGREE_OF_IMPORTANT", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OWNER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OWNERName = table.Column<string>(name: "OWNER_Name", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ASSIGNER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ASSIGNERNAME = table.Column<string>(name: "ASSIGNER_NAME", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PERFORMER = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PERFORMERNAME = table.Column<string>(name: "PERFORMER_NAME", type: "nvarchar(256)", maxLength: 256, nullable: true),
                    COLLABRATORS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLLABRATORNAMES = table.Column<string>(name: "COLLABRATOR_NAMES", type: "nvarchar(max)", nullable: true),
                    MEMBERS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RESTRICTCOMPLETE = table.Column<bool>(name: "RESTRICT_COMPLETE", type: "bit", nullable: false, defaultValue: false),
                    STATUS = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    PROJECTID = table.Column<Guid>(name: "PROJECT_ID", type: "uniqueidentifier", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NOMALIZEDNAME = table.Column<string>(name: "NOMALIZED_NAME", type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FULLNAME = table.Column<string>(name: "FULL_NAME", type: "nvarchar(max)", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LEVEL = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    PARENTID = table.Column<Guid>(name: "PARENT_ID", type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_CspWorks");
        }
    }
}
