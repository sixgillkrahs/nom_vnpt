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

namespace CSP_NET.Work.ProjectRole
{
    public class ProjectRole: Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string Domain { get; set; }
        public bool Status { get; set; }


        public ProjectRole()
        {

        }
        public ProjectRole(Guid guid) : base(guid)
        {

        }

        internal ProjectRole(Guid guid, [NotNull] string code, [NotNull] string name, string domain, bool status) : base(guid)
        {
            SetCode(code);
            SetName(name);
            Domain = domain;
            Status = status;
        }
        private void SetCode([NotNull] string code)
        {
            Code = Check.NotNullOrWhiteSpace(
                code,
                nameof(code)
            //maxLength: GenerateCodeConsts.MaxCodeLength
            );
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name)
            //maxLength: GenerateCodeConsts.MaxNameLength
            );
        }
        internal ProjectRole ChangeCode([NotNull] string code)
        {
            SetCode(code);
            return this;
        }
        internal ProjectRole ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
    }
}
