using CSP_NET.Work.Common;
using CSP_NET.Work.WorkManagement;
using CTIN.Abp;
using CTIN.Abp.Domain.Entities;
using CTIN.Abp.Domain.Entities.Auditing;
using CTIN.Abp.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSP_NET.Work.ProjectGenerals
{
    public class ProjectGeneral : FullAuditedEntity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DepartmentID { get; set; }
        public OrganizationUnit Department { get; set; }
        public string? Description { get; set; }
        public Guid? ProjectStateID { get; set; }
        public ProjectState.ProjectState ProjectState { get; set; }
        public bool Status { get; set; }
        public List<CspWork> CspWorks { get; set; }

        public ProjectGeneral()
        {
        }
        public ProjectGeneral(Guid guid) : base(guid)
        {

        }
        public ProjectGeneral(
            Guid id,
            string code,
            string name,
            string description,
            Guid departmentID,
            OrganizationUnit department,
            Guid projectStateID,
            ProjectState.ProjectState projectState,
            bool status
        ) : base(id)
        {
            Code = code;
            Name = name;
            Description = description;
            DepartmentID = departmentID;
            Department = department;
            ProjectStateID = projectStateID;
            ProjectState = projectState;
            Status = status;
        }
        internal ProjectGeneral(Guid guid, [NotNull] string code, [NotNull] string name, Guid departmentID, string? description, Guid? projectStateID, bool status) : base(guid)
        {
            SetCode(code);
            SetName(name);
            DepartmentID = departmentID;
            Description = description;
            ProjectStateID = projectStateID;
            Status = status;
        }
        private void SetCode([NotNull] string code)
        {
            Code = Check.NotNullOrWhiteSpace(
                code,
                nameof(code),
                maxLength: ProjectGeneralConsts.MaxCodeLength
            );
        }
        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: ProjectGeneralConsts.MaxNameLength
            );
        }
        internal ProjectGeneral ChangeCode([NotNull] string code)
        {
            SetCode(code);
            return this;
        }
        internal ProjectGeneral ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }
    }
}
