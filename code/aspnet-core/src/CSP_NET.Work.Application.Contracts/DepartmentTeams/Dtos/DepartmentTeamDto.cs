using System;
using System.Collections.Generic;
using CTIN.Abp.Application.Dtos;
using static CTIN.Abp.Identity.Settings.IdentitySettingNames;
using CTIN.Abp.Domain.Entities;
using CTIN.Abp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using CSP_NET.Work.Common;
using System.Diagnostics.CodeAnalysis;
using CTIN.Abp;
using CTIN.Abp.Identity;
using CSP.Category.CategoryManagement;
namespace CSP_NET.Work.DepartmentTeams.Dtos;


public class DepartmentTeamDto : EntityDto<Guid>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid DepartmentID { get; set; }

    public string DepartmentName { get; set; }

    public RecordStatusCode Status { get; set; }
    public List<NameValue<Guid>> LstMember { get; set; }
}
public class DepartmentTeamListDto : EntityDto<Guid>
{
    public string Code { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid DepartmentID { get; set; }

    public string DepartmentName { get; set; }

    public RecordStatusCode Status { get; set; }
}
