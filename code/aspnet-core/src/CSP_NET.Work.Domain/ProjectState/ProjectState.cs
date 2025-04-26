using CSP_NET.Work.Common;
using CTIN.Abp;
using CTIN.Abp.Domain.Entities;
using CTIN.Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSP_NET.Work.ProjectState
{
    public class ProjectState : FullAuditedEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }


        //protected ProjectState()
        //{
        //}

        //public ProjectState(
        //    Guid id,
        //    string code,
        //    string name
        //) : base(id)
        //{
        //    Code = code;
        //    Name = name;
        //}
        //}

        public ProjectState()
        {

        }
        public ProjectState(Guid guid) : base(guid)
        {

        }

        internal ProjectState(Guid guid, [NotNull] string code, [NotNull] string name) : base(guid)
        {
            SetCode(code);
            SetName(name);

        }
        private void SetCode([NotNull] string code)
        {
            Code = Check.NotNullOrWhiteSpace(
                code,
                nameof(code),
                maxLength: ProjectStateConsts.MaxCodeLength
            );
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: ProjectStateConsts.MaxNameLength
            );
        }
        internal ProjectState ChangeCode([NotNull] string code)
        {
            SetCode(code);
            return this;
        }
        internal ProjectState ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
    }
}
