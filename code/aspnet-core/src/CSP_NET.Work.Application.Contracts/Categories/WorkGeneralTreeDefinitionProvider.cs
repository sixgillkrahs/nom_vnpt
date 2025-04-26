using CSP.Category.CategoryManagement;
using CSP.Category.GeneralTreeManagement;
using CSP_NET.Work.Localization;
using CTIN.Abp.Guids;
using CTIN.Abp.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using static CSP.Category.Permissions.CategoryPermissions;

namespace CSP_NET.Work.Categories
{
    public class WorkGeneralTreeDefinitionProvider : GeneralTreeDefinitionProvider
    {
        private readonly IGuidGenerator _guidGenerator;

        public WorkGeneralTreeDefinitionProvider(IGuidGenerator guidGenerator)
        {
            _guidGenerator = guidGenerator;
        }

        public override void Define(IGeneralTreeDefinitionContext context)
        {
            var workType = context.Add(
                code: "Work_Cat_WorkType",
                displayName: L("Category:WorkType"),
            permissionConfig: new GeneralTreePermissionConfig(
                GeneralTreePermission.Default,
                GeneralTreePermission.Create,
                GeneralTreePermission.Edit,
                GeneralTreePermission.Delete));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<WorkResource>(name);
        }
    }
}
