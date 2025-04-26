using CTIN.Abp.Application.Dtos;
using CTIN.Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSP_NET.Work.ResourseManagement.Dtos
{
    public class DocumentDto:EntityDto<Guid>
    {
        public string Name { get; set; }
        public Guid WorkId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public long FileSize { get; set; }
        public string FileUrl { get; set; }
        public string MimeType { get; set; }

    }
}
