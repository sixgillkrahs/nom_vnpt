using CSP.Category.CategoryManagement;
using CSP.Category.Localization;
using CSP_NET.Work.Localization;
using CTIN.Abp.Localization;
using CTIN.Abp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;
using static CSP.Category.Permissions.CategoryPermissions;

namespace CSP_NET.Work.Categories;

public class WorkCategoryDefinitionProvider : CategoryDefinitionProvider
{
    public override void Define(ICategoryDefinitionContext context)
    {
        var priority = context.AddGroup(
            name: "Work_Cat_Priority",
            displayName: L("Category:Priority"),
            permissionConfig: new CategoryGroupPermissionConfig(
                CategoryPermission.Default,
                CategoryPermission.Create,
                CategoryPermission.Edit,
                CategoryPermission.Delete),
            valueConfig: ValueConfigDefine.Code,
            displayConfig: DisplayConfigDefine.Name);

        var conplexity = context.AddGroup(
            name: "Work_Cat_Complexity",
            displayName: L("Category:Complexity"),
            permissionConfig: new CategoryGroupPermissionConfig(
                CategoryPermission.Default,
                CategoryPermission.Create,
                CategoryPermission.Edit,
                CategoryPermission.Delete),
            valueConfig: ValueConfigDefine.Code,
            displayConfig: DisplayConfigDefine.Name);

        var degreeOfImportant = context.AddGroup(
            name: "Work_Cat_DegreeOfImportant",
            displayName: L("Category:DegreeOfImportant"),
            permissionConfig: new CategoryGroupPermissionConfig(
                CategoryPermission.Default,
                CategoryPermission.Create,
                CategoryPermission.Edit,
                CategoryPermission.Delete),
            valueConfig: ValueConfigDefine.Code,
            displayConfig: DisplayConfigDefine.Name);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WorkResource>(name);
    }
}
