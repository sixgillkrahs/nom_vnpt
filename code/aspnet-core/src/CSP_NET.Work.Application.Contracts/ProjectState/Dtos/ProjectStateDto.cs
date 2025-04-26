using System;
using CTIN.Abp.Application.Dtos;

namespace CSP_NET.Work.ProjectState.Dtos;

[Serializable]
public class ProjectStateDto : FullAuditedEntityDto<Guid>
{
    public string Code { get; set; }

    public string Name { get; set; }
}
