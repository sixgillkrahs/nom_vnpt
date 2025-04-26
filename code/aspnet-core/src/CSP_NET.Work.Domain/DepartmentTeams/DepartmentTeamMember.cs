using CTIN.Abp.Domain.Entities;
using CTIN.Abp.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using static CTIN.Abp.Identity.Settings.IdentitySettingNames;

namespace CSP_NET.Work.DepartmentTeamMembers
{
    public class DepartmentTeam_Members
    {
        public Guid UserId { get; set; }
        public Guid DepartmentTeamId { get; set; }

        public IdentityUser User { get; set; }
        //public DepartmentTeams DepartmentTeam { get; set; }
    }
}
