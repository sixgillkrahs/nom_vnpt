using System;
using System.Collections.Generic;
using System.Text;
using CTIN.Abp.Authorization.Permissions;
using CTIN.Abp.Localization;
using CSP_NET.Work.Localization;

namespace CSP_NET.Work.Permissions;
/// <summary>
/// Define global scopes
/// </summary>
public static class MyprojectNameScopes
{
    public static List<PermissionScopeKeyword> Scopes = new List<PermissionScopeKeyword>()
    {
        //new PermissionScopeKeyword("Root", L("Scope:Root"))
    };
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WorkResource>(name);
    }
}
