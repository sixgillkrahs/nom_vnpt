using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class CSPTree : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CSP_CAT_Category",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "OrderIndex",
                table: "CSP_CAT_Category",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CSP_CAT_Category",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AbpOrganizationUnitTypes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "CSP_CAT_GeneralTrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CSP_CAT_GeneralTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CSP_CAT_GeneralTrees_CSP_CAT_GeneralTrees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CSP_CAT_GeneralTrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CSP_CAT_GeneralTrees_IsEnabled",
                table: "CSP_CAT_GeneralTrees",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_CSP_CAT_GeneralTrees_ParentId",
                table: "CSP_CAT_GeneralTrees",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CSP_CAT_GeneralTrees_RootCode",
                table: "CSP_CAT_GeneralTrees",
                column: "RootCode");

            migrationBuilder.CreateIndex(
                name: "IX_CSP_CAT_GeneralTrees_RootCode_IsEnabled",
                table: "CSP_CAT_GeneralTrees",
                columns: new[] { "RootCode", "IsEnabled" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CSP_CAT_GeneralTrees");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CSP_CAT_Category",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "OrderIndex",
                table: "CSP_CAT_Category",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CSP_CAT_Category",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AbpOrganizationUnitTypes",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
