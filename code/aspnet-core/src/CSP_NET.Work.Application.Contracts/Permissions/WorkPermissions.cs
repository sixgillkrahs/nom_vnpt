using CTIN.Abp.Authorization.Permissions;
using CTIN.Abp.Localization;
using CSP_NET.Work.Localization;
using System.Collections.Generic;

namespace CSP_NET.Work.Permissions;

public static class WorkPermissions
{
    public const string GroupName = "Work";

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WorkResource>(name);
    }
    public class ProjectGeneral
    {
        public const string Default = GroupName + ".ProjectGeneral";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class DepartmentTeam
    {
        public const string Default = GroupName + ".DepartmentTeam";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class ProjectState
    {
        public const string Default = GroupName + ".ProjectState";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    public class CspWork
    {
        public const string Default = GroupName + ".CspWork";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
