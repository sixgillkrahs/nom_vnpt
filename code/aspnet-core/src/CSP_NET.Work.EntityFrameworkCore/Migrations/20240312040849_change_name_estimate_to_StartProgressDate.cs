using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class changenameestimatetoStartProgressDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ESTIMATE",
                table: "WMS_CspWorks",
                newName: "START_PROGRESS_DATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "START_PROGRESS_DATE",
                table: "WMS_CspWorks",
                newName: "ESTIMATE");
        }
    }
}
