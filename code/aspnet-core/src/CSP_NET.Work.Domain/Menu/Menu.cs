using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CTIN.Abp.Domain.Entities;
using CTIN.Abp.MultiTenancy;

namespace CSP_NET.Work.Menu;
public class Menu : Entity<Guid>, IMultiTenant
{
    public string? RouterLink { get; set; }

    public string? Url { get; set; }

    public string Label { get; set; }

    // thứ tự của menu item trong parent
    public int? Order { get; set; }

    public string? IconClass { get; set; }

    public string? RequiredPolicy { get; set; }

    public string? Layout { get; set; }

    public Guid? ParentId { get; set; }

    public bool IsGroup { get; set; }

    public Guid? TenantId { get; protected set; }

    public string ClientId { get; set; }
}
