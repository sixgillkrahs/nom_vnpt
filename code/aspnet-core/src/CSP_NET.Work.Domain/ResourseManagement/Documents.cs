using CSP_NET.Work.WorkManagement;
using CTIN.Abp.BlobStoring;
using CTIN.Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_NET.Work.ResourseManagement
{
   
    public class Documents : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public CspWork CspWork { get; set; }
        public Guid WorkId { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public long FileSize { get; set; }

        public string MimeType { get; set; }


        //public Guid? TenantId { get; set; }

        protected Documents()
        {
        }

        //public Documents(
        //    Guid id,
        //    long fileSize,
        //    string mimeType

        //    //Guid? tenantId
        //) : base(id)
        //{
        //    FileSize = fileSize;
        //    MimeType = mimeType;
        //    //TenantId = tenantId;
        //}

        public Documents(Guid id, string name, Guid workId, DateTime updateDate, string updateBy, long fileSize, string mimeType):base(id) 
        {
            Name = name;
            WorkId = workId;
            UpdateDate = updateDate;
            UpdateBy = updateBy;
            FileSize = fileSize;
            MimeType = mimeType;
        }
    }
}
