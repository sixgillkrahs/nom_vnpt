using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSPNET.Work.Migrations
{
    /// <inheritdoc />
    public partial class notificationconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationInfos",
                table: "NotificationInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CSP_CAT_Category",
                table: "CSP_CAT_Category");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "CTIN.AbpNotificationServiceNotifications");

            migrationBuilder.RenameTable(
                name: "NotificationInfos",
                newName: "CTIN.AbpNotificationServiceNotificationInfos");

            migrationBuilder.RenameTable(
                name: "CSP_CAT_Category",
                newName: "CSP_CAT_Categories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CSP_CAT_GeneralTrees",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CSP_CAT_Categories",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CTIN.AbpNotificationServiceNotifications",
                table: "CTIN.AbpNotificationServiceNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CTIN.AbpNotificationServiceNotificationInfos",
                table: "CTIN.AbpNotificationServiceNotificationInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSP_CAT_Categories",
                table: "CSP_CAT_Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MailSettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SmtpHost = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpPort = table.Column<int>(type: "int", nullable: false),
                    SmtpUserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpDomain = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpEnableSsl = table.Column<bool>(type: "bit", nullable: false),
                    SmtpUseDefaultCredentials = table.Column<bool>(type: "bit", nullable: false),
                    DefaultFromAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultFromDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationFactories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConfigured = table.Column<bool>(type: "bit", nullable: false),
                    SubjectTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodyTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelSampleJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_NotificationFactories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TemplateContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InlineLocalized = table.Column<bool>(type: "bit", nullable: false),
                    IsLayout = table.Column<bool>(type: "bit", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultCultureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsHtml = table.Column<bool>(type: "bit", nullable: false),
                    TemplateCultures = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextTemplates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailSettings_Tag",
                table: "MailSettings",
                column: "Tag",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TextTemplates_TemplateName",
                table: "TextTemplates",
                column: "TemplateName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailSettings");

            migrationBuilder.DropTable(
                name: "NotificationFactories");

            migrationBuilder.DropTable(
                name: "TextTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CTIN.AbpNotificationServiceNotifications",
                table: "CTIN.AbpNotificationServiceNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CTIN.AbpNotificationServiceNotificationInfos",
                table: "CTIN.AbpNotificationServiceNotificationInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CSP_CAT_Categories",
                table: "CSP_CAT_Categories");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CSP_CAT_GeneralTrees");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CSP_CAT_Categories");

            migrationBuilder.RenameTable(
                name: "CTIN.AbpNotificationServiceNotifications",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "CTIN.AbpNotificationServiceNotificationInfos",
                newName: "NotificationInfos");

            migrationBuilder.RenameTable(
                name: "CSP_CAT_Categories",
                newName: "CSP_CAT_Category");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationInfos",
                table: "NotificationInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CSP_CAT_Category",
                table: "CSP_CAT_Category",
                column: "Id");
        }
    }
}
