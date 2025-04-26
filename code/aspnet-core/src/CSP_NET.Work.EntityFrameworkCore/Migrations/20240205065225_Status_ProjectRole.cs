using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class StatusProjectRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "WMS_PROJECT_ROLES",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "WMS_PROJECT_ROLES");
        }
    }
}
