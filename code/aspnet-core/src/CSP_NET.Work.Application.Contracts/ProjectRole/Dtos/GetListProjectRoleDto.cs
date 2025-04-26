using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.ProjectRole.Dtos
{
    public class GetListProjectRoleDto : PagedAndSortedResultRequestDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }

        public string? Domain { get; set; }
        public bool? Status { get; set; }

        public string? Filter { get; set; }

    }
    public class GetAllProjectRoleDto : ISortedResultRequest
    {
        public string? Filter { get; set; }
        public string? Sorting { get; set; }
    }

}
