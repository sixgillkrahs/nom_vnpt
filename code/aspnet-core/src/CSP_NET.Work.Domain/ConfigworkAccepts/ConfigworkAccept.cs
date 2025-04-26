
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
using CSP_NET.Work.ProjectManagement;
using CTIN.Abp.Identity;
using CSP_NET.Work.WorkManagement;
using CTIN.Abp.Domain.Entities.Auditing;

namespace CSP_NET.Work.ConfigworkAccepts
{
    public class ConfigWorkAccept : AuditedEntity<Guid>
    {
        public  Guid WorkId { get; set; }
        public  Guid UserAccept1 { get; set; }
        public  DateTime? DateAccept1 { get; set; }
        public  Guid? UserAccept2 { get; set; }
        public  DateTime? DateAccept2 { get; set; }
        public WorkAcceptStatus StatusAccept1 { get; set; }
        public WorkAcceptStatus StatusAccept2 { get; set; }
        public bool Accept2 { get; set; }
        public CspWork CspWork { get; set; }

        public ConfigWorkAccept()
        {
        }

       
    }

 
}
