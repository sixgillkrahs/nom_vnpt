using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class Create_departmentteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "AppPROJECT_ROLES",
                newName: "WMS_PROJECT_ROLES");

            migrationBuilder.RenameTable(
                name: "AppPROJECT_GENERAL",
                newName: "WMS_PROJECT_GENERAL");

            migrationBuilder.CreateTable(
                name: "WMS_DEPARTMENT_TEAMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DEPARTMENT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STATUS = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523502", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_TEAM_ORGANIZATION",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WMS_DEPARTMENT_TEAM_MEMBER",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("SYS_C435324523505", x => new { x.DepartmentTeamId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_TEAM_MEMBER",
                        column: x => x.DepartmentTeamId,
                        principalTable: "WMS_DEPARTMENT_TEAMS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DEPARTMENT_TEAM_USER",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WMS_DEPARTMENT_TEAM_MEMBER_UserId",
                table: "WMS_DEPARTMENT_TEAM_MEMBER",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WMS_DEPARTMENT_TEAMS_DEPARTMENT_ID",
                table: "WMS_DEPARTMENT_TEAMS",
                column: "DEPARTMENT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WMS_DEPARTMENT_TEAM_MEMBER");

            migrationBuilder.DropTable(
                name: "WMS_DEPARTMENT_TEAMS");

            migrationBuilder.RenameTable(
                name: "WMS_PROJECT_ROLES",
                newName: "AppPROJECT_ROLES");

            migrationBuilder.RenameTable(
                name: "WMS_PROJECT_GENERAL",
                newName: "AppPROJECT_GENERAL");
        }
    }
}
