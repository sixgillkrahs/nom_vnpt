using CSP_NET.Work.Common;
using CTIN.Abp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.WorkManagement
{
    [Serializable]
    public class GetListWorkOfUserDto : PagedAndSortedResultRequestDto
    {
        public string? Keyword { get; set; }
        public string? WorkCode { get; set; }
        public string? TaskCode { get; set; }
        public string Priority { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? ParentId { get; set; }
        public CspWorkStatus? Status { get; set; }
        public bool onlyRootNode { get; set; }
        public List<Guid>? childIds { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? AssignerId { get; set; }
        public Guid? PerformerId { get; set; }

        public Guid? CollabratorId { get; set; }

    }
}
