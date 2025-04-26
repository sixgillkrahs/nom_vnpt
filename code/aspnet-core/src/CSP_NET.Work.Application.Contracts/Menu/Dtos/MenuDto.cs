using System;
using System.Collections.Generic;
using System.Text;
using CTIN.Abp.Application.Dtos;


namespace CSP_NET.Work.Menu.Dtos;
public class MenuDto : EntityDto<Guid>
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

    public string ClientId { get; set; }
}