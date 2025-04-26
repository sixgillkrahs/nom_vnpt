using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class addcolprojectNametocspworks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "WMS_CONFIGWORK_ACCEPTS",
                newName: "WORKID");

            migrationBuilder.AddColumn<string>(
                name: "PROJECT_NAME",
                table: "WMS_CspWorks",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROJECT_NAME",
                table: "WMS_CspWorks");

            migrationBuilder.RenameColumn(
                name: "WORKID",
                table: "WMS_CONFIGWORK_ACCEPTS",
                newName: "WorkId");
        }
    }
}
