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
namespace CSP_NET.Work.DepartmentTeams
{
    public class DepartmentTeam : Entity<Guid>
    {
        private string status;

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid DepartmentID { get; set; }
        public OrganizationUnit Department { get; set; }
        public RecordStatusCode Status { get; set; }
        public ICollection<DepartmentTeamMember> Members { get; set; }


        public DepartmentTeam()
        {
        }

        public DepartmentTeam(
            Guid id,
            string code,
            string name,
            string description,
            Guid departmentID,
            OrganizationUnit department,
            RecordStatusCode status,
            List<Guid> lstUserId
        ) : base(id)
        {
            Code = code;
            Name = name;
            Description = description;
            DepartmentID = departmentID;
            Department = department;
            Status = status;
            if (lstUserId != null)
            {
                Members = lstUserId.Select(a => new DepartmentTeamMember
                {
                    UserId = a,
                    DepartmentTeamId = id
                }).ToList();
            }
        }


        public DepartmentTeam(Guid id, string code, string name, string description, Guid departmentID, string status) : base(id)
        {
        }
    }

    public class DepartmentTeamMember 
    {
        public Guid UserId { get; set; }
        public IdentityUser Member { get; set; }
        public Guid DepartmentTeamId {  get; set; }
    }
}
