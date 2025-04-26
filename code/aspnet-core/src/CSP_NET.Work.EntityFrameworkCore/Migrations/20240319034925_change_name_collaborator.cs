using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class changenamecollaborator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "COLLABRATOR_NAMES",
                table: "WMS_CspWorks",
                newName: "COLLABORATOR_NAMES");

            migrationBuilder.RenameColumn(
                name: "COLLABRATORS",
                table: "WMS_CspWorks",
                newName: "COLLABORATORS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "COLLABORATOR_NAMES",
                table: "WMS_CspWorks",
                newName: "COLLABRATOR_NAMES");

            migrationBuilder.RenameColumn(
                name: "COLLABORATORS",
                table: "WMS_CspWorks",
                newName: "COLLABRATORS");
        }
    }
}
