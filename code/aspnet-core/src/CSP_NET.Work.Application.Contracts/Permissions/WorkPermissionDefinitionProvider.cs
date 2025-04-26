using CSP_NET.Work.Localization;
using CTIN.Abp.Localization;
using CTIN.Abp.MultiTenancy;
using CTIN.Abp.Authorization.Permissions;

namespace CSP_NET.Work.Permissions;

public class WorkPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WorkPermissions.GroupName);


        var projectGeneralPermission = myGroup.AddPermission(WorkPermissions.ProjectGeneral.Default, L("Permission:ProjectGeneral"));
        projectGeneralPermission.AddChild(WorkPermissions.ProjectGeneral.Create, L("Permission:Create"));
        projectGeneralPermission.AddChild(WorkPermissions.ProjectGeneral.Update, L("Permission:Update"));
        projectGeneralPermission.AddChild(WorkPermissions.ProjectGeneral.Delete, L("Permission:Delete"));

        var departmentTeamPermission = myGroup.AddPermission(WorkPermissions.DepartmentTeam.Default, L("Permission:DepartmentTeam"));
        departmentTeamPermission.AddChild(WorkPermissions.DepartmentTeam.Create, L("Permission:Create"));
        departmentTeamPermission.AddChild(WorkPermissions.DepartmentTeam.Update, L("Permission:Update"));
        departmentTeamPermission.AddChild(WorkPermissions.DepartmentTeam.Delete, L("Permission:Delete"));

        var projectStatePermission = myGroup.AddPermission(WorkPermissions.ProjectState.Default, L("Permission:ProjectState"));
        projectStatePermission.AddChild(WorkPermissions.ProjectState.Create, L("Permission:Create"));
        projectStatePermission.AddChild(WorkPermissions.ProjectState.Update, L("Permission:Update"));
        projectStatePermission.AddChild(WorkPermissions.ProjectState.Delete, L("Permission:Delete"));

        var cspWorkPermission = myGroup.AddPermission(WorkPermissions.CspWork.Default, L("Permission:CspWork"));
        cspWorkPermission.AddChild(WorkPermissions.CspWork.Create, L("Permission:Create"));
        cspWorkPermission.AddChild(WorkPermissions.CspWork.Update, L("Permission:Update"));
        cspWorkPermission.AddChild(WorkPermissions.CspWork.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WorkResource>(name);
    }
}
