using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.ProjectRole.Dtos
{
    public class ProjectRoleDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string Domain { get; set; }
        public bool Status { get; set; }

    }
}
