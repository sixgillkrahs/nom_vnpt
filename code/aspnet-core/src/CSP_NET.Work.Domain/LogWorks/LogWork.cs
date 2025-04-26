using CSP_NET.Work.Common;
using CTIN.Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP_NET.Work.WorkManagement;
namespace CSP_NET.Work.LogWorks
{
    public class LogWork : AuditedEntity<Guid>
    {
        public Guid WorkId { get; set; }
        public Guid UserId { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public StatusLogWork StatusLogWork { get; set; }

        public CspWork CspWork { get; set; }

        public LogWork()
        {
        }


    }
}
